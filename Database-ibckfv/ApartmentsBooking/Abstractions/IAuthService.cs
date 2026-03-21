using ApartmentsBooking.Models;
using ApartmentsBooking.RequestEntities;

namespace ApartmentsBooking.Abstractions;

public interface IAuthService
{
    public Task<User?> Login(LoginRequest req);
    public Task<User> Register(RegisterRequest req);
    public Task<User> UpdateUserInfo(UpdateUserRequest req);
    public Task DeleteProfile(User user);
}