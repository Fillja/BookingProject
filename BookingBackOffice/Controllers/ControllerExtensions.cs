using Microsoft.AspNetCore.Mvc;

namespace BookingBackOffice.Controllers;

public static class ControllerExtensions
{
    public static void SetError(this Controller controller, string message) =>
        controller.TempData["ErrorMessage"] = message;

    public static void SetSuccess(this Controller controller, string message) =>
        controller.TempData["DisplayMessage"] = message;
}
