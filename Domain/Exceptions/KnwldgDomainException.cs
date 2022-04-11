namespace Domain.Exceptions
{
    public class KnwldgDomainException : Exception
    {
        public KnwldgDomainException()
        {
        }

        public KnwldgDomainException(string? message)
            : base(message)
        {
        }

        public KnwldgDomainException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }
    }
}
