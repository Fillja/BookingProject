using BookingBackOffice.Models;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookingBackOffice.Controllers;

public class BookingController(IBookingService bookingService, ITableService tableService, BookingRepository bookingRepository) : Controller
{
    private readonly IBookingService _bookingService = bookingService;
    private readonly ITableService _tableService = tableService;
    private readonly BookingRepository _bookingRepository = bookingRepository;

    public async Task<IActionResult> Index()
    {
        var bookingModel = new BookingViewModel();

        var tableListResult = await _tableService.GetAllTablesWithBookingsAsync("Restaurant1");
        if (tableListResult.HasFailed)
        {
            bookingModel.ErrorMessage = tableListResult.Message;
            return View(bookingModel);
        }

        var bookingListResult = await _bookingService.GetAllBookingsAsync("Restaurant1");
        if (bookingListResult.HasFailed)
        {
            bookingModel.ErrorMessage = bookingListResult.Message;
            return View(bookingModel);
        }

        var tableList = ((List<TableModel>)tableListResult.Content!).OrderBy(x => x.Name).ToList();
        var bookingList = ((List<BookingModel>)bookingListResult.Content!).OrderBy(x => x.BookingStartTime).ToList();

        bookingModel.Tables = tableList;
        bookingModel.Bookings = bookingList;

        return View(bookingModel);
    }

    [HttpPost]
    public async Task<IActionResult> EditBooking(BookingModel model, string action)
    {
        TempData["DisplayMessage"] = "You must fill out all the necessary fields!";

        if (ModelState.IsValid) 
        {
            if (action == "update")
            {
                var updateResult = await _bookingService.UpdateBookingAsync(model.Id!, model);
                TempData["DisplayMessage"] = updateResult.Message;
            }
            else if (action == "delete")
            {
                var deleteResult = await _bookingRepository.DeleteAsync(x => x.Id == model.Id);
                TempData["DisplayMessage"] = deleteResult.Message;
            }

            return RedirectToAction("Index");
        }

        return RedirectToAction("Index");
    }
}
