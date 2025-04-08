using Microsoft.AspNetCore.Mvc;

namespace BookingBackOffice.Controllers;

public class TableController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
