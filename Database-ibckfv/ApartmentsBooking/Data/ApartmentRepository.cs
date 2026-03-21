using ApartmentsBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace ApartmentsBooking.Data;

public class ApartmentRepository : IRepostiory<Apartment>
{
    private readonly AppDbContext _dbContext;

    public ApartmentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Apartment>> GetAllAsync()
    {
        return await _dbContext.Apartments.ToListAsync();
    }

    public async Task<Apartment?> GetAsync(int id)
    {
        return await _dbContext.Apartments.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task CreateAsync(Apartment item)
    {
        _dbContext.Apartments.Add(item);
        await SaveAsync();
    }

    public async Task UpdateAsync(Apartment item)
    {
        _dbContext.Apartments.Update(item);
        await SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var apartment = await _dbContext.Apartments.FirstOrDefaultAsync(u => u.Id == id);
        if (apartment == null)
        {
            throw new Exception("apartment not found");
        }
        _dbContext.Apartments.Remove(apartment);
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