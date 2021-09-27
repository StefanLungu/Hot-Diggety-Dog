using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Security.Helpers;
using Security.Services;
using Security.Settings;

namespace Security
{
    public static class SecurityDI
    {
        public static void AddSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IJwtService), typeof(JwtService))
                .AddScoped(typeof(IFacebookAuthService), typeof(FacebookAuthService))
                .Configure<SecuritySettings>(configuration.GetSection("SecuritySettings"))
                .Configure<FacebookAuthSettings>(configuration.GetSection("FacebookSettings"));
        }
    }
}
