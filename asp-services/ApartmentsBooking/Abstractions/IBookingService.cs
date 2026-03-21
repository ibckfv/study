using ApartmentsBooking.Models;

namespace ApartmentsBooking.Abstractions;

public interface IBookingService
{
    public Task<Booking> Book(Booking book);
    public Task DeleteBooking(Booking book);
    public Task<List<Booking>> GetUserAllBookings(User user);
    public Task<List<Booking>> GetAllActiveBookings();
}