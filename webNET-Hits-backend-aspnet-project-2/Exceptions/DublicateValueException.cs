namespace webNET_Hits_backend_aspnet_project_2.Exceptions
{
    public class DublicateValueException : Exception
    {
        public DublicateValueException()
            : base("The value dublicate already existing one in database")
        {
        }

        public DublicateValueException(string message)
            : base(message)
        {
        }
    }
}
