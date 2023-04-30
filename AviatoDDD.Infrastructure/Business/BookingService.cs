using AviatoDDD.Domain.Configurations;
using AviatoDDD.Domain.Repositories;
using AviatoDDD.Domain.Services;
using Microsoft.Extensions.Configuration;

namespace AviatoDDD.Repository.Business;

public class BookingService: IBookingService
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IConfiguration _configuration;
    private readonly BookingProperties _bookingProperties;

    public BookingService(IBookingRepository bookingRepository, IConfiguration configuration)
    {
        _bookingRepository = bookingRepository;
        _configuration = configuration;
        _bookingProperties = _configuration.GetSection("BookingProperties").Get<BookingProperties>();
    }
}