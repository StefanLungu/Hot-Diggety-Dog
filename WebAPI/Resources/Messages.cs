namespace WebApi.Resources
{
    public static class Messages
    {
        public const string InvalidCredentials = "Invalid credentials.";
        public const string DuplicateUsernameOrEmail = "A user with the same username or email already exists.";
        public const string InvalidEmail = "Invalid email.";
        public const string PasswordLengthError = "The Password field must be a minimum of 6 characters.";
        public const string InvalidData = "Invalid data.";
        public const string FacebookLoginFailed = "Something went wrong! (invalid facebook token)";
        public const string NotEnoughProductsInInventory = "There are not enough products in inventory";

        public static string NotFoundMessage(string entityName, System.Guid entityId)
        {
            return $"{entityName} with id {entityId} not found.";
        }
        public static string StandByOperatorNotFoundMessage(System.Guid operatorId)
        {
            return $"{EntitiesConstants.HotDogStandEntity} with operator id {operatorId} not found.";
        }
        public static string StandProductByProductIdNotFoundMessage(System.Guid standId, System.Guid productId)
        {
            return $"{EntitiesConstants.StandProductEntity} with stand id {standId} and product id {productId} not found.";
        }
    }
}
