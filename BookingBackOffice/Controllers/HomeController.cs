using BookingBackOffice.Models;
using Infrastructure.Entities;
using Infrastructure.Helpers;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookingBackOffice.Controllers;

[Authorize]
public class HomeController(IBookingService bookingService, ITableService tableService, IRestaurantService restaurantService, UserManager<UserEntity> userManager) : Controller
{
    private readonly IBookingService _bookingService = bookingService;
    private readonly ITableService _tableService = tableService;
    private readonly IRestaurantService _restaurantService = restaurantService;
    private readonly UserManager<UserEntity> _userManager = userManager;

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return RedirectToAction("SignIn", "Account");

        HomeViewModel homeModel = new HomeViewModel();


        // Restaurant handling for user & admin
        ResponseResult restaurantListResult = await _restaurantService.GetAllRestaurantsAsync();
        if(restaurantListResult.HasFailed)
            homeModel.AdminMessage = restaurantListResult.Message;

        IEnumerable<RestaurantModel> restaurantList = (IEnumerable<RestaurantModel>)restaurantListResult.Content!;
        homeModel.Restaurants = restaurantList;

        RestaurantModel userRestaurant = restaurantList.FirstOrDefault(x => x.Id == user.RestaurantId)!;
        if (userRestaurant == null)
        {
            homeModel.RestaurantName = "Restaurant associated with the user could not be found";

        }
        else
        {
            homeModel.RestaurantName = userRestaurant.Name;
        }


        // Table & Booking amounts
        ResponseResult tableListResult = await _tableService.GetAllTablesWithBookingsAsync("Restaurant1");
        if (tableListResult.HasFailed)
        {
            homeModel.ErrorMessage = tableListResult.Message;
            return View(homeModel);
        }

        ResponseResult bookingListResult = await _bookingService.GetAllBookingsAsync("Restaurant1");
        if (bookingListResult.HasFailed)
        {
            homeModel.ErrorMessage = bookingListResult.Message;
            return View(homeModel);
        }

        List<TableModel> tableList = (List<TableModel>)tableListResult.Content!;
        homeModel.Tables = tableList!.Count;

        List<BookingModel> bookingList = (List<BookingModel>)bookingListResult.Content!;
        homeModel.Bookings = bookingList!.Count;


        return View(homeModel);
    }
    
    public async Task<IActionResult> UpdateAdminRestaurant(string restaurantId)
    {
        var user = await _userManager.GetUserAsync(User);
        if(user == null)
            return RedirectToAction("SignIn", "Account");
        
        user.RestaurantId = restaurantId;
        IdentityResult updateResult = await _userManager.UpdateAsync(user);

        return RedirectToAction("Index");
    }
}
