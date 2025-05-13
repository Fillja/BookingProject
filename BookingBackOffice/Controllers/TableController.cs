using BookingBackOffice.Models;
using Infrastructure.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookingBackOffice.Controllers;

[Authorize]
public class TableController(ITableService tableService, UserManager<UserEntity> userManager) : Controller
{
    private readonly ITableService _tableService = tableService;
    private readonly UserManager<UserEntity> _userManager = userManager;

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return RedirectToAction("SignIn", "Account");

        var tablesModel = new TableViewModel();

        if (TempData["DisplayMessage"] is string displayMessage && !string.IsNullOrWhiteSpace(displayMessage))
            tablesModel.DisplayMessage = displayMessage;

        if (TempData["ErrorMessage"] is string errorMessage && !string.IsNullOrWhiteSpace(errorMessage))
            tablesModel.ErrorMessage = errorMessage;

        var tableResult = await _tableService.GetAllTablesWithBookingsAsync(user.RestaurantId);
        if (tableResult.HasFailed)
        {
            tablesModel.ErrorMessage += "\nError fetching tables: " + tableResult.Message;
        }
        else
        {
            tablesModel.Tables = (List<TableModel>)tableResult.Content!;
        }

        return View(tablesModel);
    }

    public async Task<IActionResult> SaveLayout(TableViewModel model)
    {
        bool anyFailed = false;

        foreach (var table in model.Tables)
        {
            var updateResult = await _tableService.UpdateTableAsync(table.Id!, table);
            if (updateResult.HasFailed)
            {
                anyFailed = true;
                this.SetError(updateResult.Message!);
            }
        }

        if (!anyFailed)
            this.SetSuccess("Updated successfully.");


        return RedirectToAction("Index");
    }

    public async Task<IActionResult> UpdateTable(TableModel model)
    {
        var updateResult = await _tableService.UpdateTableAsync(model.Id!, model);

        if (updateResult.HasFailed)
        {
            this.SetError(updateResult.Message!);
        }
        else
        {
            this.SetSuccess(updateResult.Message!);
        }

        return RedirectToAction("Index");
    }
}
