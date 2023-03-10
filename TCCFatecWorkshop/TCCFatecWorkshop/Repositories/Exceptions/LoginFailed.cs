namespace TCCFatecWorkshop.Repositories.Exceptions
{
    public class LoginFailed : ApplicationException
    {
        public LoginFailed(string message):base(message) { }
    }
}
