using ApartmentsBooking.Abstractions;
using ApartmentsBooking.Models;
using ApartmentsBooking.Services;
using FluentAssertions;
using Xunit;

namespace ApartmentsBooking.Tests;

public class BookingServiceTests
{
    private class InMemoryBookingRepository : IBookingRepository
    {
        private readonly List<Booking> _bookings = new();
        private int _idSeq = 1;

        public Task<Booking> Create(Booking value)
        {
            value.Id = _idSeq++;
            _bookings.Add(value);
            return Task.FromResult(value);
        }

        public Task<Booking?> Read(int id)
        {
            return Task.FromResult(_bookings.FirstOrDefault(b => b.Id == id));
        }

        public Task<Booking> Update(Booking oldVal, Booking newVal)
        {
            var index = _bookings.IndexOf(oldVal);
            _bookings[index] = newVal;
            return Task.FromResult(newVal);
        }

        public Task Delete(int id)
        {
            _bookings.RemoveAll(b => b.Id == id);
            return Task.CompletedTask;
        }

        public Task<List<Booking>> GetAll()
        {
            return Task.FromResult(_bookings.ToList());
        }
    }

    [Fact]
    public async Task Book_ShouldCreateBooking()
    {
        var repo = new InMemoryBookingRepository();
        var service = new BookingService(repo);

        var booking = new Booking
        {
            UserId = 1,
            ApartmentId = 2,
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(3),
            CreatedAt = DateTime.Now
        };

        var created = await service.Book(booking);

        created.Id.Should().BeGreaterThan(0);
        (await service.GetAllActiveBookings()).Should().ContainSingle();
    }

    [Fact]
    public async Task GetUserAllBookings_ShouldReturnOnlyUserBookings()
    {
        var repo = new InMemoryBookingRepository();
        var service = new BookingService(repo);

        await service.Book(new Booking
        {
            UserId = 1,
            ApartmentId = 1,
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(2),
            CreatedAt = DateTime.Now
        });

        await service.Book(new Booking
        {
            UserId = 2,
            ApartmentId = 2,
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(2),
            CreatedAt = DateTime.Now
        });

        var userBookings = await service.GetUserAllBookings(new User { Id = 1 });

        userBookings.Should().HaveCount(1);
        userBookings[0].UserId.Should().Be(1);
    }

    [Fact]
    public async Task GetAllActiveBookings_ShouldFilterByEndDate()
    {
        var repo = new InMemoryBookingRepository();
        var service = new BookingService(repo);

        // прошедшее бронирование
        await service.Book(new Booking
        {
            UserId = 1,
            ApartmentId = 1,
            StartDate = DateTime.Now.AddDays(-5),
            EndDate = DateTime.Now.AddDays(-1),
            CreatedAt = DateTime.Now.AddDays(-6)
        });

        // активное бронирование
        await service.Book(new Booking
        {
            UserId = 2,
            ApartmentId = 2,
            StartDate = DateTime.Now.AddDays(1),
            EndDate = DateTime.Now.AddDays(3),
            CreatedAt = DateTime.Now
        });

        var active = await service.GetAllActiveBookings();

        active.Should().HaveCount(1);
        active[0].UserId.Should().Be(2);
    }
}

