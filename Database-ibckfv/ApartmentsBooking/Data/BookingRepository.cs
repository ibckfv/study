using ApartmentsBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace ApartmentsBooking.Data;

public class BookingRepository : IRepostiory<Booking>
{
    private readonly AppDbContext _dbContext;

    public BookingRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Booking>> GetAllAsync()
    {
        return await _dbContext.Bookings.ToListAsync();
    }

    public async Task<Booking?> GetAsync(int id)
    {
        return await _dbContext.Bookings.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task CreateAsync(Booking item)
    {
        _dbContext.Bookings.Add(item);
        await SaveAsync();
    }

    public async Task UpdateAsync(Booking item)
    {
        _dbContext.Bookings.Update(item);
        await SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var booking = await _dbContext.Bookings.FirstOrDefaultAsync(u => u.Id == id);
        if (booking == null)
        {
            throw new Exception("booking not found");
        }
        _dbContext.Bookings.Remove(booking);
        await SaveAsync();
    }

    public async Task SaveAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
    
    public void Dispose()
    {
        throw new NotImplementedException();
    }
}