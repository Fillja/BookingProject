﻿using Microsoft.AspNetCore.Mvc;

namespace BookingBackOffice.Controllers;

public class BookingController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
