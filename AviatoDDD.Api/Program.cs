using AviatoDDD.Domain.Data;
using AviatoDDD.Domain.Repositories;
using AviatoDDD.Domain.Services;
using AviatoDDD.Repository.Business;
using AviatoDDD.Repository.Mappings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AviatoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AviatoConnectionString")));

// Repositories
builder.Services.AddScoped<IAirplaneRepository, AirplaneRepository>();

builder.Services.AddAutoMapper(typeof(AviatoMappingProfiles));

// Services
builder.Services.AddScoped<IAirplaneService, AirplaneService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();