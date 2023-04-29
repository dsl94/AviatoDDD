using AviatoDDD.Domain.Enums;

namespace AviatoDDD.Domain.Exceptions;

public class EntityNotFoundException: Exception
{
    public string ErrorCode { get; set; }
    
    public EntityNotFoundException(String code, string message): base(message)
    {
        ErrorCode = code;
    }
}