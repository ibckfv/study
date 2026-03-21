using ApartmentsBooking.Abstractions;
using ApartmentsBooking.Models;
using ApartmentsBooking.RequestEntities;

namespace ApartmentsBooking.Services;

public class AuthService(IUserRepository userRepository) : IAuthService
{
    public async Task<User?> Login(LoginRequest req)
    {
        return await userRepository.Read(req.Login);
    }

    public async Task<User> Register(RegisterRequest req)
    {
        return await userRepository.Create(req);
    }

    public async Task<User> UpdateUserInfo(UpdateUserRequest req)
    {
        var users = await userRepository.GetAll();
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

        return await userRepository.Update(oldUser, newUser);
    }

    public async Task DeleteProfile(User user)
    {
        await userRepository.Delete(user.Id);
    }
}