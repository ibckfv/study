using ApartmentsBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace ApartmentsBooking.Data;

public class UserRepository : IRepostiory<User>
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<User?> GetAsync(int id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task CreateAsync(User item)
    {
        _dbContext.Users.Add(item);
        await SaveAsync();
    }

    public async Task UpdateAsync(User item)
    {
        _dbContext.Users.Update(item);
        await SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
        {
            throw new Exception("user not found");
        }
        _dbContext.Users.Remove(user);
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