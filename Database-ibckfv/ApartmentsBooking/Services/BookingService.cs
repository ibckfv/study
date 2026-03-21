using ApartmentsBooking.Abstractions;
using ApartmentsBooking.Data;
using ApartmentsBooking.Models;

namespace ApartmentsBooking.Services;

public class BookingService(IRepostiory<Booking> bookingRepository) : IBookingService
{
    public async Task<Booking> Book(Booking book)
    {
        await bookingRepository.CreateAsync(book);
        return book;
    }

    public async Task DeleteBooking(Booking book)
    {
        await bookingRepository.DeleteAsync((int)book.Id);
    }

    public async Task<List<Booking>> GetUserAllBookings(User user)
    {
        var res = await bookingRepository.GetAllAsync();
        return res.Where(booking => booking.UserId == user.Id).ToList();    
    }

    public async Task<List<Booking>> GetAllActiveBookings()
    {
        var res = await bookingRepository.GetAllAsync();
        return res.Where(booking => booking.EndDate > DateTime.Now).ToList();
    }
}