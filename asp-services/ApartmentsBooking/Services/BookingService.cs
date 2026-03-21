using ApartmentsBooking.Abstractions;
using ApartmentsBooking.Models;

namespace ApartmentsBooking.Services;

public class BookingService(IBookingRepository bookingRepository) : IBookingService
{
    public async Task<Booking> Book(Booking book)
    {
        return await bookingRepository.Create(book);
    }

    public async Task DeleteBooking(Booking book)
    {
        await bookingRepository.Delete(book.Id);
    }

    public async Task<List<Booking>> GetUserAllBookings(User user)
    {
        var res = await bookingRepository.GetAll();
        return res.Where(booking => booking.UserId == user.Id).ToList();    }

    public async Task<List<Booking>> GetAllActiveBookings()
    {
        var res = await bookingRepository.GetAll();
        return res.Where(booking => booking.EndDate > DateTime.Now).ToList();
    }
}