using AviatoDDD.Domain.Configurations;
using AviatoDDD.Domain.Enums;
using AviatoDDD.Domain.Models;

namespace AviatoDDD.Repository.Utilities;

public static class OfferCalculationUtility
{
    public static float CalculatePrice(
        float basePrice,
        CustomerType customerType,
        ClassType classType,
        int pointsToUse,
        DateTime dateAndTimeOfFlight,
        BookingProperties? bookingProperties)
    {
        // ((BasePrice + (30 - 0.05% per hour)) * ClassMultiplier)) - points to user
        var finalPrice = basePrice + GetHourPercentage(dateAndTimeOfFlight, basePrice);
        finalPrice = finalPrice * GetClassMultiplier(bookingProperties, classType);
        
        // First we subtract points from original price
        finalPrice = finalPrice - pointsToUse;
        
        // Then we divide by 2 if passenger is child
        if (customerType.Equals(CustomerType.Child))
        {
            finalPrice = finalPrice / 2;
        }

        if (finalPrice < 0)
        {
            return 0;
        }

        return finalPrice;
    }

    private static float GetHourPercentage(DateTime flightTime, float basePrice)
    {
        DateTime now = DateTime.Now;
        int hours = (int)Math.Round((flightTime - now).TotalHours);
        
        var percentage = (float)(30 - (0.05 * hours));

        return basePrice * percentage / 100;
    }

    private static int GetClassMultiplier(BookingProperties? bookingProperties, ClassType classType)
    {
        switch (classType)
        {
            case ClassType.Business:
                return bookingProperties!.BusinessClassMultiplier;
            case ClassType.First:
                return bookingProperties!.FirstClassMultiplier;
            default:
                return 1;
        }
    }
}