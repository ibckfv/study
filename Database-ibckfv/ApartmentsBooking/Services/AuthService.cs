using ApartmentsBooking.Abstractions;
using ApartmentsBooking.Models;
using ApartmentsBooking.RequestEntities;
using ApartmentsBooking.Data;

namespace ApartmentsBooking.Services;

public class AuthService(IRepostiory<User> userRepository) : IAuthService
{
    public async Task<User?> Login(LoginRequest req)
    {
        var users = await userRepository.GetAllAsync();
        var user = users.FirstOrDefault(a => a.Login == req.Login);
        if (user == null)
        {
            throw new Exception("user not found");
        }
        return await userRepository.GetAsync((int)user.Id);
    }

    public async Task<User> Register(RegisterRequest req)
    {
        var newUser = new User()
        {
            CreatedAt = DateTime.Now,
            FullName = req.FullName,
            Phone = req.Phone,
            Login = req.Login,
            Password = req.Password
        };
        await userRepository.CreateAsync(newUser);
        return newUser;
    }

    public async Task<User> UpdateUserInfo(UpdateUserRequest req)
    {
        var users = await userRepository.GetAllAsync();
        var oldUser = users.FirstOrDefault(a => a.Login == req.Login);
        var newUser = new User()
        {
            Id = oldUser!.Id,
            CreatedAt = oldUser.CreatedAt,
            FullName = req.FullName ?? oldUser.FullName,
            Phone = req.Phone ?? oldUser.Phone,
            Login = oldUser.Login,
            Password = oldUser.Password
        };

        await userRepository.UpdateAsync(newUser);
        return newUser;
    }

    public async Task DeleteProfile(User user)
    {
        await userRepository.DeleteAsync((int)user.Id);
    }
}