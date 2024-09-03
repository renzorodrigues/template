namespace Zeeget.Shared.Utils.Constants
{
    public static class ContantStrings
    {
        public static class ResultMessages
        {
            public const string Created = "It has been created successfully.";
            public const string NotFound = "Item not found.";
            public const string Unauthorized = "Credentials not valid.";
            public const string BadRequest = "Request not valid.";
            public const string Error = "Something went wrong.";
            public const string Conflict = "Username and/or Email already exists.";
        }

        public static class LoggingMessages
        {
            public const string Handling = "Handling >>> {Name}";
            public const string Handled = "Handled >>> {Name}";
            public const string Exception = "{ExceptionName} : {Message} :: {StatusCode}";
            public const string UserCreatedWithId = "User {Username} created with ID {UserId}";
        }
    }
}
