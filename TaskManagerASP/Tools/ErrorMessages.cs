namespace TaskManagerASP.Tools
{
    public static class ErrorMessages
    {
        public const string NotAuthenticatedUser = "You should login first.";
        public const string ShouldBeAdmin = "You do not have enough permissions.";
        public static string NoAccess(string itemName = "item") 
            => $"You do not have access to this {itemName}.";
        public static string DoesNotExist(string itemName = "item")
            => $"This {itemName} does not exist.";

    }
}
