using AviatoDDD.Domain.Enums;

namespace AviatoDDD.Domain.DTO;

public class ErrorResponse
{
    public string ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
}