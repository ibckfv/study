using ApartmentsBooking.Abstractions;
using ApartmentsBooking.Models;
using ApartmentsBooking.RequestEntities;

namespace ApartmentsBooking.Services;

public class ApartmentService(IApartmentRepository apartmentRepository) : IApartmentService
{
    public async Task<Apartment> AddApartment(Apartment apart)
    {
        return await apartmentRepository.Create(apart);
    }

    public async Task<List<Apartment>> GetAllApartments()
    {
        return await apartmentRepository.GetAll();
    }

    public async Task<Apartment> UpdateApartment(UpdateApartmentRequest req)
    {
        var oldApartment = await apartmentRepository.Read(req.Id);
        if (oldApartment == null)
        {
            throw new InvalidOperationException($"Apartment with id '{req.Id}' not found");
        }

        var newApartment = new Apartment
        {
            Id = oldApartment.Id,
            Title = req.Title ?? oldApartment.Title,
            Description = req.Description ?? oldApartment.Description,
            PricePerDay = req.PricePerDay ?? oldApartment.PricePerDay,
            OwnerId = oldApartment.OwnerId,
            Floor = req.Floor ?? oldApartment.Floor,
            TotalArea = req.TotalArea ?? oldApartment.TotalArea,
            Rooms = req.Rooms ?? oldApartment.Rooms,
            CreatedAt = oldApartment.CreatedAt
        };

        return await apartmentRepository.Update(oldApartment, newApartment);
    }

    public async Task DeleteApartment(Apartment apart)
    {
        await apartmentRepository.Delete(apart.Id);
    }
}