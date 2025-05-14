using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

//DI Repositories
builder.Services.AddScoped<RestaurantRepository>();
builder.Services.AddScoped<TableRepository>();
builder.Services.AddScoped<BookingRepository>();


//DI Services
builder.Services.AddScoped<IRestaurantService, RestaurantService>();
builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddScoped<IBookingService, BookingService>();

//Identity
builder.Services.AddScoped<UserManager<UserEntity>>();
builder.Services.AddScoped<SignInManager<UserEntity>>();

builder.Services.AddIdentity<UserEntity, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/SignIn";
    options.LogoutPath = "/Account/SignOut";
    options.AccessDeniedPath = "/Home/Index";
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


//Forcing en-US invariance, to handle floats/double decimal points
var cultureInfo = new CultureInfo("en-US");
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(cultureInfo),
    SupportedCultures = new List<CultureInfo> { cultureInfo },
    SupportedUICultures = new List<CultureInfo> { cultureInfo }
};
app.UseRequestLocalization(localizationOptions);

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=SignIn}/{id?}");

app.UseStatusCodePagesWithRedirects("/Home/Error");

app.Run();
