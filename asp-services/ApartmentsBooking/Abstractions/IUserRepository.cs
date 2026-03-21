using ApartmentsBooking.Models;
using ApartmentsBooking.RequestEntities;

namespace ApartmentsBooking.Abstractions;

public interface IUserRepository
{
    public Task<User> Create(RegisterRequest req);
    public Task<User?> Read(string? login);
    public Task<User> Update(User oldVal, User newVal);
    public Task Delete(int id);
    public Task<List<User>> GetAll();
}