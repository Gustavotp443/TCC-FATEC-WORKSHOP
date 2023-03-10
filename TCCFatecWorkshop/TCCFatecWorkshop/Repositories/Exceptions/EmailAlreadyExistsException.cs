namespace TCCFatecWorkshop.Repositories.Exceptions
{
    public class EmailAlreadyExistsException : ApplicationException
    {
        public EmailAlreadyExistsException(string message):base(message) { }
    }
}
