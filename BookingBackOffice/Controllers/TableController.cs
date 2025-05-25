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
public class TableController(ITableService tableService, TableRepository tableRepository, UserManager<UserEntity> userManager) : Controller
{
    private readonly ITableService _tableService = tableService;
    private readonly TableRepository _tableRepository = tableRepository;
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

    public async Task<IActionResult> CreateTable()
    {
        var tableModel = new TableModel
        {
            RestaurantId = (await _userManager.GetUserAsync(User))?.RestaurantId ?? string.Empty,
            Name = "New table",
            Size = 4,
            TopAlignment = 0f,
            LeftAlignment = 0f
        };
        var createResult = await _tableService.CreateTableAsync(tableModel);
        if(createResult.HasFailed)
        {
            this.SetError(createResult.Message!);
        }
        else
        {
            this.SetSuccess(createResult.Message!);
        }

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

    public async Task<IActionResult> DeleteTable(string deleteId)
    {
        var deleteResult = await _tableRepository.DeleteAsync(x => x.Id == deleteId);

        if(deleteResult.HasFailed)
        {
            this.SetError(deleteResult.Message!);
        }
        else
        {
            this.SetSuccess("Table deleted successfully.");
        }

        return RedirectToAction("Index");
    }
}
