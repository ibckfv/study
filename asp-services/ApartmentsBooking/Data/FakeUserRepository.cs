using ApartmentsBooking.Abstractions;
using ApartmentsBooking.Models;
using ApartmentsBooking.RequestEntities;

namespace ApartmentsBooking.Data;

public class FakeUserRepository : IUserRepository
{
    private int _idSeq = 4;
    private readonly object _locker = new object();
    private readonly List<User> _users = 
    [
        new User()
        {
            Id = 1,
            Login = "User1",
            Phone = "0001",
            Password = "Password1",
            FullName = "First",
            CreatedAt = DateTime.Now
        },
        new User()
        {
            Id = 2,
            Login = "User2",
            Phone = "0002",
            Password = "Password2",
            FullName = "Second",
            CreatedAt = DateTime.Now
        },
        new User()
        {
            Id = 3,
            Login = "User3",
            Phone = "0003",
            Password = "Password3",
            FullName = "Third",
            CreatedAt = DateTime.Now
        }
    ];
    
    public Task<User> Create(RegisterRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.Login))
            throw new ArgumentException("Login cannot be empty");
    
        if (string.IsNullOrWhiteSpace(req.Password))
            throw new ArgumentException("Password cannot be empty");

        lock (_locker)
        {
            if (_users.Any(u => u.Login == req.Login))
            {
                return Task.FromException<User>(
                    new InvalidOperationException($"User with login '{req.Login}' already exists")
                );
            }

            var user = new User
            {
                Id = _idSeq,
                CreatedAt = DateTime.Now,
                Phone = req.Phone,
                FullName = req.FullName,
                Login = req.Login,
                Password = req.Password
            };

            _users.Add(user);
            _idSeq++;
        
            return Task.FromResult(user);
        }
    }

    public Task<User?> Read(string? login)
    {
        lock (_locker)
        {
            var user = _users.FirstOrDefault(a => a.Login == login);
            return Task.FromResult(user);
        }    
    }

    public Task<User?> Read(int id)
    {
        lock (_locker)
        {
            var user = _users.FirstOrDefault(a => a.Id == id);
            return Task.FromResult(user);
        }
    }

    public Task<User> Update(User? oldVal, User? newVal)
    {
        lock (_locker)
        {
            if (oldVal == null)
            {
                return Task.FromException<User>(new InvalidOperationException("User not found"));
            }
            
            var index = _users.IndexOf(oldVal);
            _users[index] = newVal!;
            
            return Task.FromResult(newVal);
        }
    }

    public Task Delete(int id)
    {
        lock (_locker)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return Task.FromException(new InvalidOperationException($"User with id '{id}' not found"));
            }

            _users.Remove(user);
            return Task.CompletedTask;
        } 
    }

    public Task<List<User>> GetAll()
    {
        lock (_locker)
        {
            return Task.FromResult(_users);
        }
    }
}