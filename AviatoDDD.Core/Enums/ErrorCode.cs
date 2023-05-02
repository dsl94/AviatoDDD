namespace AviatoDDD.Domain.Enums;

public class ErrorCode
{
    public const string GeneralError = "GENERAL_ERROR";
    public const string EntityNotFound = "ENTITY_NOT_FOUND";
    public const string BadRequest = "BAD_REQUEST";
    public const string NotEnoughPoints = "NOT_ENOUGH_POINTS";
    public const string TooLateToBook = "TOO_LATE_TO_BOOK";
    public const string AlreadyHasBooking = "CUSTOMER_ALREADY_HAS_BOOKING";
    public const string FlightFull = "FLIGHT_FULL";
    public const string AlreadyConfirmed = "ALREADY_CONFIRMED";
    public const string OfferExpired = "OFFER_EXPIRED";
}