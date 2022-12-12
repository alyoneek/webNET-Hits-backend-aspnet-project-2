namespace webNET_Hits_backend_aspnet_project_2.Exceptions
{
    public class MismatchedValuesException : Exception
    {
        public MismatchedValuesException()
            : base("The value does not match what is in the database")
        {
        }

        public MismatchedValuesException(string message)
            : base(message)
        {
        }
    }
}
