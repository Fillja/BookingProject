using BookingBackOffice.Models;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingBackOffice.Controllers;

public class TableController(ITableService tableService) : Controller
{
    private readonly ITableService _tableService = tableService;

    public async Task<IActionResult> Index()
    {
        var tablesModel = new TableViewModel();

        if (TempData["DisplayMessage"] is string displayMessage && !string.IsNullOrWhiteSpace(displayMessage))
            tablesModel.DisplayMessage = displayMessage;

        if (TempData["ErrorMessage"] is string errorMessage && !string.IsNullOrWhiteSpace(errorMessage))
            tablesModel.ErrorMessage = errorMessage;

        var tableResult = await _tableService.GetAllTablesWithBookingsAsync("Restaurant1");
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
}
