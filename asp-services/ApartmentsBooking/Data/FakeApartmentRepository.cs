using ApartmentsBooking.Abstractions;
using ApartmentsBooking.Models;

namespace ApartmentsBooking.Data;

public class FakeApartmentRepository : IApartmentRepository
{
    private readonly object _locker = new object();
    private readonly List<Apartment> _apartments = 
    [
        new Apartment()
        {
            Id = 1,
            Title = "First",
            Description = "",
            PricePerDay = 2000,
            OwnerId = 1,
            Floor = 15,
            TotalArea = 50,
            Rooms = 2,
            CreatedAt = DateTime.Today
        },
        
        new Apartment()
        {
            Id = 2,
            Title = "Second",
            Description = "",
            PricePerDay = 3000,
            OwnerId = 2,
            Floor = 25,
            TotalArea = 60,
            Rooms = 3,
            CreatedAt = DateTime.Today
        },
        
        new Apartment()
        {
            Id = 3,
            Title = "Third",
            Description = "",
            PricePerDay = 4000,
            OwnerId = 3,
            Floor = 35,
            TotalArea = 70,
            Rooms = 3,
            CreatedAt = DateTime.Today
        }
    ];
        
    public Task<Apartment> Create(Apartment value)
    {
        lock (_locker)
        {
            _apartments.Add(value);
            return Task.FromResult(value);
        }
    }

    public Task<Apartment?> Read(int id)
    {
        lock (_locker)
        {
            var apartment = _apartments.FirstOrDefault(a => a.Id == id);
            return Task.FromResult(apartment);
        }
    }

    public Task<Apartment> Update(Apartment oldVal, Apartment newVal)
    {
        lock (_locker)
        {
            if (!_apartments.Contains(oldVal))
            {
                return Task.FromException<Apartment>(new InvalidOperationException("Apartment not found"));
            }
            var index = _apartments.IndexOf(oldVal);
            _apartments[index] = newVal;
            return Task.FromResult(newVal);
        }
    }

    public Task Delete(int id)
    {
        lock (_locker)
        {
            var user = _apartments.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return Task.FromException(new InvalidOperationException($"Apt with id '{id}' not found"));
            }

            _apartments.Remove(user);
            return Task.CompletedTask;
        }
    }

    public Task<List<Apartment>> GetAll()
    {
        lock (_locker)
        {
            return Task.FromResult(_apartments);
        }
    }
}