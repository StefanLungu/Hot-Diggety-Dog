using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Security.Helpers;
using Security.Resources;
using System.Net.Http;
using System.Threading.Tasks;

namespace Security.Services
{
    public class FacebookAuthService : IFacebookAuthService
    {
        private readonly FacebookAuthSettings _facebookAuthSettings;
        private readonly IHttpClientFactory _httpClientFactory;

        public FacebookAuthService(IOptions<FacebookAuthSettings> facebookAuthSettings, IHttpClientFactory httpClientFactory)
        {
            _facebookAuthSettings = facebookAuthSettings.Value;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<FacebookUserInfoResult> GetUserInfoAsync(string accessToken)
        {
            var formattedUrl = string.Format(Constants.FacebookGetUserInfoURL, accessToken, _facebookAuthSettings.AppId, _facebookAuthSettings.AppSecret);

            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);

            result.EnsureSuccessStatusCode();

            var responseAsString = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<FacebookUserInfoResult>(responseAsString);
        }

        public async Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToken)
        {
            var formattedUrl = string.Format(Constants.FacebookTokenValidationURL, accessToken, _facebookAuthSettings.AppId, _facebookAuthSettings.AppSecret);

            var result = await _httpClientFactory.CreateClient().GetAsync(formattedUrl);

            result.EnsureSuccessStatusCode();

            var responseAsString = await result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<FacebookTokenValidationResult>(responseAsString);
        }
    }
}
