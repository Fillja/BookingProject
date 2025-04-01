using BookingBackOffice.Models.Home;
using Microsoft.AspNetCore.Mvc;

namespace BookingBackOffice.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var homeModel = new HomeViewModel
            {
                RestaurantName = "The Restaurant",
                Bookings = 10,
                Seatings = 20
            };
            return View(homeModel);
        }

    }
}
