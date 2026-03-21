using ApartmentsBooking.Models;
using ApartmentsBooking.RequestEntities;

namespace ApartmentsBooking.Abstractions;

public interface IApartmentService
{
    public Task<Apartment> AddApartment(Apartment apart);
    public Task<List<Apartment>> GetAllApartments();
    public Task<Apartment> UpdateApartment(UpdateApartmentRequest req);
    public Task DeleteApartment(Apartment apart);
}