using ApartmentsBooking.Abstractions;
using ApartmentsBooking.RequestEntities;
using ApartmentsBooking.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentsBooking.Controllers;

[ApiController]
[Route("api/apartments")]
public class ApartmentController(IApartmentService apartmentService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllApartments()
    {
        var apartments = await apartmentService.GetAllApartments();
        return Ok(apartments);
    }
    
    [HttpPost("new")]
    [Authorize]
    public async Task<IActionResult> CreateApartment([FromBody] Apartment apartment)
    {
        var all = await apartmentService.GetAllApartments();
        apartment.Id = all.Any() ? all.Max(a => a.Id) + 1 : 1;
        apartment.CreatedAt = DateTime.Now;
        
        var created = await apartmentService.AddApartment(apartment);
        return Ok(created);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetApartmentById(int id)
    {
        var apartments = await apartmentService.GetAllApartments();
        var apartment = apartments.FirstOrDefault(a => a.Id == id);
        
        if (apartment == null)
        {
            return NotFound();
        }

        return Ok(apartment);
    }

    [HttpPut("update/{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateApartment(int id, [FromBody] UpdateApartmentRequest request)
    {
        if (id != request.Id)
        {
            return BadRequest("Route id and body id do not match");
        }

        var updated = await apartmentService.UpdateApartment(request);
        return Ok(updated);
    }
}