namespace TCCFatecWorkshop.Repositories.Exceptions
{
    public class UsernameAlreadyExistsException : ApplicationException
    {
        public UsernameAlreadyExistsException(string message):base(message) { }
    }
}
