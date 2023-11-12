namespace TestTask
{
    public class CustomExceptions
    {
        public class CustomBadRequestException : Exception
        {
            public CustomBadRequestException(string message) : base(message)
            {
            }
        }
    }
}
