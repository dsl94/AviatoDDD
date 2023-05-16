using AviatoDDD.Domain.DTO.Booking;

namespace AviatoDDD.Commands;

public class CreateBookingOfferCommand: ICommand
{
    public CreateBookingOfferDTO BookingOffer { get; set; }
}