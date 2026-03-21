using ApartmentsBooking.Abstractions;
using ApartmentsBooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentsBooking.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingController(IBookingService bookingService) : ControllerBase
{
    [HttpGet("active")]
    [Authorize]
    public async Task<IActionResult> GetAllActiveBookings()
    {
        var bookings = await bookingService.GetAllActiveBookings();
        return Ok(bookings);
    }
    
    [HttpPost("new")]
    public async Task<IActionResult> CreateBooking([FromBody] Booking booking)
    {
        booking.CreatedAt = DateTime.Now;
        var created = await bookingService.Book(booking);
        return Ok(created);
    }

    [HttpPut("user/{id}")]
    [Authorize]
    public async Task<IActionResult> GetBookingByUser(int id)
    {
        var user = new User { Id = id };
        var bookings = await bookingService.GetUserAllBookings(user);
        return Ok(bookings);
    }
}