﻿using BookingApi.Filters;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[UseApiKey]
public class BookingController(BookingRepository bookingRepository, IBookingService bookingService) : ControllerBase
{
    private readonly BookingRepository _bookingRepository = bookingRepository;
    private readonly IBookingService _bookingService = bookingService;

    [HttpPost("create")]
    public async Task<IActionResult> Create(BookingModel bookingModel)
    {
        if (ModelState.IsValid)
        {
            var createResult = await _bookingService.CreateBookingAsync(bookingModel);

            if (createResult.StatusCode.Equals(0))
                return Created($"api/booking/create/{createResult.Content}", createResult.Content);

            else if (createResult.StatusCode.Equals(3))
                return Conflict(createResult.Message);

            else if (createResult.StatusCode.Equals(2))
                return NotFound(createResult.Message);

            return BadRequest(createResult.Message);
        }

        return BadRequest("Invalid fields.");
    }

    [HttpGet("getall/{restaurantId}")]
    public async Task<IActionResult> GetAll(string restaurantId)
    {
        var listResult = await _bookingService.GetAllBookingsAsync(restaurantId);

        if (listResult.StatusCode.Equals(0))
            return Ok(listResult.Content);

        else if (listResult.StatusCode.Equals(2))
            return NotFound(listResult.Message);

        return BadRequest(listResult.Message);
    }

    [HttpGet("getone/{id}")]
    public async Task<IActionResult> GetOne(string id)
    {
        var getBookingResult = await _bookingService.GetBookingAsync(id);

        if (getBookingResult.StatusCode.Equals(0))
            return Ok(getBookingResult.Content);

        else if (getBookingResult.StatusCode.Equals(2))
            return NotFound(getBookingResult.Message);

        return BadRequest(getBookingResult.Message);
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(string id, BookingModel bookingModel)
    {
        if (ModelState.IsValid)
        {
            var updateResult = await _bookingService.UpdateBookingAsync(id, bookingModel);

            if (updateResult.StatusCode.Equals(0))
                return Ok(updateResult.Content);

            else if (updateResult.StatusCode.Equals(2))
                return NotFound(updateResult.Message);

            return BadRequest(updateResult.Message);
        }

        return BadRequest("Invalid fields.");
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleteResult = await _bookingRepository.DeleteAsync(x => x.Id == id);

        if (deleteResult.StatusCode.Equals(0))
            return Ok(deleteResult.Message);

        else if (deleteResult.StatusCode.Equals(2))
            return NotFound(deleteResult.Message);

        return BadRequest(deleteResult.Message);
    }
}
