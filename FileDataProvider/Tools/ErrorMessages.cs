namespace FileDataProvider.Tools
{
    public static class ErrorMessages
    {
        public static string CannotBeNegative(string field = "This field") => $"{field} cannot be negative.";
        public static string CannotBeNullOrEmpty(string field = "This field") => $"{field} cannot be null or empty.";
    }
}
