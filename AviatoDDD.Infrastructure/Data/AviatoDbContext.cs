using AviatoDDD.Domain.Enums;
using AviatoDDD.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AviatoDDD.Domain.Data;

public class AviatoDbContext: DbContext
{
    public AviatoDbContext()
    {
        
    }
    public AviatoDbContext(DbContextOptions dbContextOptions): base(dbContextOptions)
    {
        
    }
    
    public DbSet<Airplane> Airplanes { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Flight> Flights { get; set; }
    public DbSet<Booking> Bookings { get; set; }
}