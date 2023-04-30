using AviatoDDD.Domain.Enums;
using AviatoDDD.Domain.Exceptions;

namespace AviatoDDD.Repository.Utilities;

public class OfferValidationUtility
{
    public static void ValidatePoints(int available, int requested)
    {
        if (available < requested)
        {
            throw new BookingCreationException(ErrorCode.NotEnoughPoints,
                "Customer has requested " + requested + " points to use but ony has " + available +
                " points available");
        }
    }

    public static void ValidateIfFlightCanBeBookedAtThisMoment(DateTime flightTime)
    {
        DateTime now = DateTime.Now;
        int hours = (int)Math.Round((flightTime - now).TotalHours);

        if (hours <= 0)
        {
            throw new BookingCreationException(ErrorCode.TooLateToBook,
                "Flight has already departed so it can not be booked");
        }
        
        if (hours < 5)
        {
            throw new BookingCreationException(ErrorCode.TooLateToBook,
                "Flight is in less then 5 hours so it can not be booked");
        }
    }
}