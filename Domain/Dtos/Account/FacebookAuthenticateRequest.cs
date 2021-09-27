using System.ComponentModel.DataAnnotations;

namespace Domain.Dtos.Account
{
    public class FacebookAuthenticateRequest
    {
        [Required]
        public string AccessToken { get; set; }
    }
}
