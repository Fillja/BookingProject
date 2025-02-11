using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

//DI Repositories
builder.Services.AddScoped<RestaurantRepository>();
builder.Services.AddScoped<TableRepository>();
builder.Services.AddScoped<ChairRepository>();
builder.Services.AddScoped<TableChairRepository>();
builder.Services.AddScoped<BookingRepository>();


//DI Services
builder.Services.AddScoped<RestaurantService>();
builder.Services.AddScoped<ChairService>();
builder.Services.AddScoped<TableService>();
builder.Services.AddScoped<TableChairService>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();