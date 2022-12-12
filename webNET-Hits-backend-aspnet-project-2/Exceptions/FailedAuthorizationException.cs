namespace webNET_Hits_backend_aspnet_project_2.Exceptions
{
    public class FailedAuthorizationException : Exception
    {
        public FailedAuthorizationException()
            : base("Uncorrect authorization params")
        {
        }

        public FailedAuthorizationException(string message)
            : base(message)
        {
        }
    }
}
