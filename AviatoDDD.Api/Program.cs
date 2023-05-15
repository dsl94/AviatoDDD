using AviatoDDD.Domain.Data;
using AviatoDDD.Domain.Enums;
using AviatoDDD.Domain.Models;
using AviatoDDD.Domain.Repositories;
using AviatoDDD.Domain.Services;
using AviatoDDD.Middlewares;
using AviatoDDD.Repository.Business;
using AviatoDDD.Repository.Mappings;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AviatoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AviatoConnectionString")));

// Repositories
builder.Services.AddScoped<ICrudRepository<Flight>, FlightRepository>();
builder.Services.AddScoped<ICrudRepository<Customer>, CustomerRepository>();
builder.Services.AddScoped<ICrudRepository<Airplane>, AirplaneRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

builder.Services.AddAutoMapper(typeof(AviatoMappingProfiles));

// Services
builder.Services.AddScoped<IAirplaneService, AirplaneService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IBookingService, BookingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// using (var scope = app.Services.CreateScope())
// {
//     var services = scope.ServiceProvider;
//
//     var context = services.GetRequiredService<AviatoDbContext>();    
//     context.Database.Migrate();
// }
app.Run();