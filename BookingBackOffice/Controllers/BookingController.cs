using BookingBackOffice.Models;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingBackOffice.Controllers;

public class BookingController(IBookingService bookingService, ITableService tableService) : Controller
{
    private readonly IBookingService _bookingService = bookingService;
    private readonly ITableService _tableService = tableService;

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
}
