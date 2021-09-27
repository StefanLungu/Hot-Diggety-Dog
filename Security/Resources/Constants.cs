namespace Security.Resources
{
    public static class Constants
    {
        public const string UserItem = "User";
        public const string AuthorizationHeader = "Authorization";
        public const string IdField = "id";
        public const int JwtValidityInHours = 3;
        public const string FacebookTokenValidationURL = "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}|{2}";
        public const string FacebookGetUserInfoURL = "https://graph.facebook.com/me?fields=first_name,last_name,picture,email&access_token={0}";
    }
}
