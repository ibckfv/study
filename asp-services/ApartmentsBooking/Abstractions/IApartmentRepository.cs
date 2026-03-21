using ApartmentsBooking.Models;

namespace ApartmentsBooking.Abstractions;

public interface IApartmentRepository
{
    public Task<Apartment> Create(Apartment value);
    public Task<Apartment?> Read(int id);
    public Task<Apartment> Update(Apartment oldVal, Apartment newVal);
    public Task Delete(int id);
    public Task<List<Apartment>> GetAll();
}