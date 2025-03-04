using Infrastructure.Models;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingController(BookingRepository bookingRepository, BookingService bookingService) : ControllerBase
{
    private readonly BookingRepository _bookingRepository = bookingRepository;
    private readonly BookingService _bookingService = bookingService;

    [HttpPost("create")]
    public async Task<IActionResult> Create(CompositeBookingAndSeatingModel compositeModel)
    {
            var createResult = await _bookingService.CreateBookingAsync(compositeModel.Booking, compositeModel.Seating);

            if (createResult.StatusCode.Equals(0))
                return Created($"api/booking/create/{createResult.Content}", createResult.Content);

            else if (createResult.StatusCode.Equals(2))
                return NotFound(createResult.Message);

            return BadRequest(createResult.Message);
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        var listResult = await _bookingRepository.GetAllAsync();

        if(listResult.StatusCode.Equals(0))
            return Ok(listResult.Content);

        else if(listResult.StatusCode.Equals(2))
            return NotFound(listResult.Message);

        return BadRequest(listResult.Message);
    }

    [HttpGet("getone/{id}")]
    public async Task<IActionResult> GetOne(string id)
    {
        var getBookingResult = await _bookingService.GetOneBookingAsync(id);

        if(getBookingResult.StatusCode.Equals(0))
            return Ok(getBookingResult.Content);

        else if(getBookingResult.StatusCode.Equals(2))
            return NotFound(getBookingResult.Message);

        return BadRequest(getBookingResult.Message);
    }

    //[HttpPut("update/{id}")]
    //public async Task<IActionResult> Update(string id)
    //{

    //}

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleteResult = await _bookingRepository.DeleteAsync(x => x.Id == id);

        if(deleteResult.StatusCode.Equals(0))
            return Ok(deleteResult.Message);

        else if(deleteResult.StatusCode.Equals(2))
            return NotFound(deleteResult.Message);

        return BadRequest(deleteResult.Message);
    }
}
