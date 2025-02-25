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

            if (createResult.StatusCode == Infrastructure.Helpers.StatusCode.CREATED)
                return Created($"api/booking/create/{createResult.Content}", createResult);

            else if (createResult.StatusCode == Infrastructure.Helpers.StatusCode.NOT_FOUND)
                return NotFound(createResult.Message);

            return BadRequest(createResult.Message);
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        var listResult = await _bookingRepository.GetAllAsync();

        if(listResult.StatusCode == Infrastructure.Helpers.StatusCode.OK)
            return Ok(listResult);

        else if(listResult.StatusCode == Infrastructure.Helpers.StatusCode.NOT_FOUND)
            return NotFound(listResult.Message);

        return BadRequest(listResult.Message);
    }

    [HttpGet("getone/{id}")]
    public async Task<IActionResult> GetOne(string id)
    {
        var getBookingResult = await _bookingRepository.GetOneAsync(x => x.Id == id);

        if(getBookingResult.StatusCode == Infrastructure.Helpers.StatusCode.OK)
            return Ok(getBookingResult);

        else if(getBookingResult.StatusCode == Infrastructure.Helpers.StatusCode.NOT_FOUND)
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

        if(deleteResult.StatusCode == Infrastructure.Helpers.StatusCode.OK)
            return Ok(deleteResult.Message);

        else if(deleteResult.StatusCode == Infrastructure.Helpers.StatusCode.NOT_FOUND)
            return NotFound(deleteResult.Message);

        return BadRequest(deleteResult.Message);
    }
}
