namespace TCCFatecWorkshop.Repositories.Exceptions
{
    public class NameAlreadyExistsException:ApplicationException
    {
        public NameAlreadyExistsException(string message) : base(message) { }
    }
}

