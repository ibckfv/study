using ApartmentsBooking.Abstractions;
using ApartmentsBooking.Models;
using ApartmentsBooking.RequestEntities;
using ApartmentsBooking.Services;
using FluentAssertions;
using Xunit;

namespace ApartmentsBooking.Tests;

public class AuthServiceTests
{
    private class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users = new();
        private int _idSeq = 1;

        public Task<User> Create(RegisterRequest req)
        {
            var user = new User
            {
                Id = _idSeq++,
                Login = req.Login,
                Password = req.Password,
                Phone = req.Phone,
                FullName = req.FullName,
                CreatedAt = DateTime.Now
            };
            _users.Add(user);
            return Task.FromResult(user);
        }

        public Task<User?> Read(string? login)
        {
            return Task.FromResult(_users.FirstOrDefault(u => u.Login == login));
        }

        public Task<User> Update(User oldVal, User newVal)
        {
            var index = _users.IndexOf(oldVal);
            _users[index] = newVal;
            return Task.FromResult(newVal);
        }

        public Task Delete(int id)
        {
            _users.RemoveAll(u => u.Id == id);
            return Task.CompletedTask;
        }

        public Task<List<User>> GetAll()
        {
            return Task.FromResult(_users.ToList());
        }
    }

    [Fact]
    public async Task Register_ShouldCreateUser()
    {
        var repo = new InMemoryUserRepository();
        var service = new AuthService(repo);
        var request = new RegisterRequest
        {
            Login = "test",
            Password = "pwd",
            FullName = "Test User",
            Phone = "123"
        };

        var user = await service.Register(request);

        user.Id.Should().BeGreaterThan(0);
        user.Login.Should().Be("test");
    }

    [Fact]
    public async Task Login_ShouldReturnUser_WhenCredentialsExist()
    {
        var repo = new InMemoryUserRepository();
        var service = new AuthService(repo);

        var registered = await service.Register(new RegisterRequest
        {
            Login = "test",
            Password = "pwd"
        });

        var loggedIn = await service.Login(new LoginRequest
        {
            Login = "test",
            Password = "pwd"
        });

        loggedIn.Should().NotBeNull();
        loggedIn!.Id.Should().Be(registered.Id);
    }

    [Fact]
    public async Task Login_ShouldReturnNull_WhenUserNotFound()
    {
        var repo = new InMemoryUserRepository();
        var service = new AuthService(repo);

        var loggedIn = await service.Login(new LoginRequest
        {
            Login = "unknown",
            Password = "pwd"
        });

        loggedIn.Should().BeNull();
    }
}

