using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Security.Resources;
using Security.Settings;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly SecuritySettings _securitySettings;

        public JwtMiddleware(RequestDelegate next, IOptions<SecuritySettings> securitySettings)
        {
            _next = next;
            _securitySettings = securitySettings.Value;
        }

        public async Task Invoke(HttpContext context, IUsersRepository repository)
        {
            string token = context.Request.Headers[Constants.AuthorizationHeader].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                Guid userId = ExtractUserIdFromToken(token);
                if (!userId.Equals(Guid.Empty))
                {
                    context.Items[Constants.UserItem] = await repository.GetByIdAsync(userId);
                }
            }

            await _next(context);
        }

        private Guid ExtractUserIdFromToken(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new();
                byte[] key = Encoding.ASCII.GetBytes(_securitySettings.Secret);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;
                return Guid.Parse(jwtToken.Claims.First(x => x.Type == Constants.IdField).Value);
            }
            catch
            {
                // do nothing if JWT validation fails
                // user is not attached to context so request won't have access to secure routes
            }
            return Guid.Empty;
        }
    }
}
