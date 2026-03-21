using ApartmentsBooking.Models;

namespace ApartmentsBooking.Abstractions;

public interface IBookingRepository
{
    public Task<Booking> Create(Booking value);
    public Task<Booking?> Read(int id);
    public Task<Booking> Update(Booking oldVal, Booking newVal);
    public Task Delete(int id);
    public Task<List<Booking>> GetAll();
}