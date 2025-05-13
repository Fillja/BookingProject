using BookingBackOffice.Models;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookingBackOffice.Controllers;

[Authorize]
public class BookingController(IBookingService bookingService, ITableService tableService, BookingRepository bookingRepository, UserManager<UserEntity> userManager) : Controller
{
    private readonly IBookingService _bookingService = bookingService;
    private readonly ITableService _tableService = tableService;
    private readonly BookingRepository _bookingRepository = bookingRepository;
    private readonly UserManager<UserEntity> _userManager = userManager;

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return RedirectToAction("SignIn", "Account");

        var bookingModel = new BookingViewModel();

        if (TempData["DisplayMessage"] is string displayMessage && !string.IsNullOrWhiteSpace(displayMessage))
            bookingModel.DisplayMessage = displayMessage;

        if (TempData["ErrorMessage"] is string errorMessage && !string.IsNullOrWhiteSpace(errorMessage))
            bookingModel.ErrorMessage = errorMessage;


        var tableListResult = await _tableService.GetAllTablesWithBookingsAsync(user.RestaurantId);
        if (tableListResult.HasFailed)
        {
            bookingModel.ErrorMessage += "\nError fetching tables: " + tableListResult.Message;
        }
        else
        {
            bookingModel.Tables = ((List<TableModel>)tableListResult.Content!).OrderBy(x => x.Name).ToList();
        }
            

        var bookingListResult = await _bookingService.GetAllBookingsAsync(user.RestaurantId);
        if (bookingListResult.HasFailed)
        {
            bookingModel.ErrorMessage += "\nError fetching bookings: " + bookingListResult.Message;
        }
        else
        {
            bookingModel.Bookings = ((List<BookingModel>)bookingListResult.Content!).OrderBy(x => x.BookingStartTime).ToList();
        }
            

        return View(bookingModel);
    }

    [HttpPost]
    public async Task<IActionResult> EditBooking(BookingModel model, string action)
    {
        if (ModelState.IsValid) 
        {
            if (action == "update")
            {
                var updateResult = await _bookingService.UpdateBookingAsync(model.Id!, model);
                if (updateResult.HasFailed)
                {
                    this.SetError(updateResult.Message!);
                }
                else
                {
                    this.SetSuccess(updateResult.Message!);
                }
            }
            else if (action == "delete")
            {
                var deleteResult = await _bookingRepository.DeleteAsync(x => x.Id == model.Id);
                if (deleteResult.HasFailed)
                {
                    this.SetError(deleteResult.Message!);
                }
                else
                {
                    this.SetSuccess(deleteResult.Message!);
                }
            }
        }
        else
        {
            this.SetError("You must fill out all the necessary fields!");
        }

        return RedirectToAction("Index");
    }
}
