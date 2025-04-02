//using BookingBackOffice.Models.Home;
//using Infrastructure.Entities;
//using Infrastructure.Interfaces;
//using Infrastructure.Models;
//using Infrastructure.Repositories;
//using Microsoft.AspNetCore.Mvc;

//namespace BookingBackOffice.Controllers
//{
//    public class HomeController(IBookingService bookingService, ISeatingService seatingService, BookingRepository bookingRepository) : Controller
//    {
//        private readonly IBookingService _bookingService = bookingService;
//        private readonly ISeatingService _seatingService = seatingService;
//        private readonly BookingRepository _bookingRepository = bookingRepository;

//        public async Task<IActionResult> Index()
//        {
//            var homeModel = new HomeViewModel();
//            homeModel.RestaurantName = "Italli";

//            var seatingListResult = await _seatingService.GetAllSeatingsAsync("Restaurant1");
//            if (seatingListResult.HasFailed)
//            {
//                homeModel.ErrorMessage = seatingListResult.Message;
//                return View(homeModel);
//            }

//            var bookingListResult = await _bookingRepository.GetAllAsync("Restaurant1");
//            if (bookingListResult.HasFailed)
//            {
//                homeModel.ErrorMessage = bookingListResult.Message;
//                return View(homeModel);
//            }

//            var seatingList = (List<SeatingModel>)seatingListResult.Content!;
//            homeModel.Seatings = seatingList!.Count;

//            var bookingList = (List<BookingEntity>)bookingListResult.Content!;
//            homeModel.Bookings = bookingList!.Count;


//            return View(homeModel);
//        }

//    }
//}
