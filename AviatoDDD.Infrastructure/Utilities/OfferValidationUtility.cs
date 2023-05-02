using AviatoDDD.Domain.Enums;
using AviatoDDD.Domain.Exceptions;
using AviatoDDD.Domain.Models;

namespace AviatoDDD.Repository.Utilities;

public static class OfferValidationUtility
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
    
    public static void CheckIfFlightHasFreeSeats(Flight flight, ClassType classType)
    {
        var usedSeats = 0;
        var itHasSeats = true;
        foreach (var booking in flight.Bookings)
        {
            if (booking.ClassType.Equals(classType) && booking.BookingStatus.Equals(BookingStatus.Confirmed))
            {
                usedSeats++;
            }
        }

        switch (classType)
        {
            case ClassType.Economy:
                itHasSeats = usedSeats < flight.Airplane.EconomyClassCapacity;
                break;
            case ClassType.Business:
                itHasSeats = usedSeats < flight.Airplane.BusinessClassCapacity;
                break;
            case ClassType.First:
                itHasSeats = usedSeats < flight.Airplane.FirstClassCapacity;
                break;
            default:
                itHasSeats = false;
                break;
        }

        if (!itHasSeats)
        {
            throw new BookingCreationException(ErrorCode.FlightFull, "All seats are sold out for requested flight");
        }
    }

    public static void ValidateIfOfferCanBeAccepted(Booking booking)
    {
        if (booking.BookingStatus.Equals(BookingStatus.Confirmed))
        {
            throw new BookingCreationException(ErrorCode.AlreadyConfirmed, "Booking is already confirmed and can not be accepted");
        }
        ValidateIfFlightCanBeBookedAtThisMoment(booking.Flight.DateAndTime);
        CheckIfFlightHasFreeSeats(booking.Flight, booking.ClassType);
        ValidateIfOfferExpired(booking.CreatedAt);
    }

    public static void ValidateIfOfferCanBeDeclined(Booking booking)
    {
        if (booking.BookingStatus.Equals(BookingStatus.Confirmed))
        {
            throw new BookingCreationException(ErrorCode.AlreadyConfirmed, "Booking is already confirmed and can not be declined");
        }
    }

    private static void ValidateIfOfferExpired(DateTime bookingTime)
    {
        if (DateTime.Now > bookingTime.AddMinutes(10))
        {
            throw new BookingCreationException(ErrorCode.OfferExpired, "Booking offer expired and it is deleted, please create new one");
        }
    }
}