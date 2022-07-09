namespace GameForum.Application.Security.Models
{
    public class SignInResult
    {
        public bool Succeeded { get; protected set; }
        public bool IsLockedOut { get; protected set; }
        public bool IsNotAllowed { get; protected set; }

        public static SignInResult Success = new SignInResult()
        {
            Succeeded = true,
            IsLockedOut = false,
            IsNotAllowed = false
        };

        public static SignInResult Fail = new SignInResult()
        {
            Succeeded = false,
            IsLockedOut = false,
            IsNotAllowed = true
        };
    }
}
