using ApartmentsBooking.Abstractions;
using ApartmentsBooking.Models;

namespace ApartmentsBooking.Data;

public class FakeBookingRepository : IBookingRepository
{
    private readonly object _locker = new object();
    private readonly List<Booking> _bookings = 
    [
        new Booking()
        {
            Id = 1,
            UserId = 1,
            ApartmentId = 1,
            CreatedAt = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            StartDate = DateTime.Now.AddDays(2),
        },
        
        new Booking()
        {
            Id = 2,
            UserId = 2,
            ApartmentId = 2,
            CreatedAt = DateTime.Now,
            EndDate = DateTime.Now.AddDays(2),
            StartDate = DateTime.Now.AddDays(3),
        },
        
        new Booking()
        {
            Id = 3,
            UserId = 3,
            ApartmentId = 3,
            CreatedAt = DateTime.Now,
            EndDate = DateTime.Now.AddDays(3),
            StartDate = DateTime.Now.AddDays(4),
        }
    ];
    
    public Task<Booking> Create(Booking value)
    {
        lock (_locker)
        {
            _bookings.Add(value);
            return Task.FromResult(value);
        }
    }

    public Task<Booking?> Read(int id)
    {
        lock (_locker)
        {
            var booking = _bookings.FirstOrDefault(a => a.Id == id);
            return Task.FromResult(booking);
        }
    }

    public Task<Booking> Update(Booking oldVal, Booking newVal)
    {
        lock (_locker)
        {
            if (!_bookings.Contains(oldVal))
            {
                return Task.FromException<Booking>(new InvalidOperationException("Booking not found"));
            }
            var index = _bookings.IndexOf(oldVal);
            _bookings[index] = newVal;
            return Task.FromResult(newVal);
        }
    }

    public Task Delete(int id)
    {
        lock (_locker)
        {
            var user = _bookings.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return Task.FromException(new InvalidOperationException($"Booking with id '{id}' not found"));
            }

            _bookings.Remove(user);
            return Task.CompletedTask;
        }
    }

    public Task<List<Booking>> GetAll()
    {
        lock (_locker)
        {
            return Task.FromResult(_bookings);
        }
    }
}