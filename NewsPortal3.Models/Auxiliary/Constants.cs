using System;

namespace NewsPortal3.Models.Auxiliary
{
    public static class Constants
    {
        public const int PageSize = 10;
        
        public static class Roles
        {
            public const string Administrator = "Administrator";
            public const string Editor = "Editor";
            public const string Reader = "Reader";
            public static readonly string[] All = new string[3] { Administrator, Editor, Reader };
        }
        public static class Errors
        {
            public const string NotFound = "Not Found Exception";
            public const string Auth = "Authentication Error";
        }
        public static class Answers
        {
            public const string Success = "Success";
        }
    }
}
