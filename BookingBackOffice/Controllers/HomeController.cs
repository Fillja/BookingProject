using BookingBackOffice.Models.Home;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingBackOffice.Controllers
{
    public class HomeController(IBookingService bookingService, ITableService tableService) : Controller
    {
        private readonly IBookingService _bookingService = bookingService;
        private readonly ITableService _tableService = tableService;

        public async Task<IActionResult> Index()
        {
            var homeModel = new HomeViewModel();
            homeModel.RestaurantName = "Michaelangelo";

            var tableListResult = await _tableService.GetAllTablesWithBookingsAsync("Restaurant2");
            if (tableListResult.HasFailed)
            {
                homeModel.ErrorMessage = tableListResult.Message;
                return View(homeModel);
            }

            var bookingListResult = await _bookingService.GetAllBookingsAsync("Restaurant2");
            if (bookingListResult.HasFailed)
            {
                homeModel.ErrorMessage = bookingListResult.Message;
                return View(homeModel);
            }

            var tableList = (List<TableModel>)tableListResult.Content!;
            homeModel.Tables = tableList!.Count;

            var bookingList = (List<BookingModel>)bookingListResult.Content!;
            homeModel.Bookings = bookingList!.Count;


            return View(homeModel);
        }

    }
}
