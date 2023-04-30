namespace AviatoDDD.Domain.Exceptions;

public class BookingCreationException: Exception
{
    public string ErrorCode { get; set; }
    
    public BookingCreationException(String code, string message): base(message)
    {
        ErrorCode = code;
    }
}