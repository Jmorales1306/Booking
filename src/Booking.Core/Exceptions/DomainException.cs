namespace Booking.Core.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string? message, string? code = null, string? target = null)
            : base(message)
        {
            Code = code ?? message;
            Target = target;
        }

        public string? Code { get; }
        public string? Target { get; }
    }


}
