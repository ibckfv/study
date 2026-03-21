using ApartmentsBooking.Abstractions;
using ApartmentsBooking.Models;
using ApartmentsBooking.RequestEntities;
using ApartmentsBooking.Services;
using FluentAssertions;
using Xunit;

namespace ApartmentsBooking.Tests;

public class ApartmentServiceTests
{
    private class InMemoryApartmentRepository : IApartmentRepository
    {
        private readonly List<Apartment> _apartments = new();
        private int _idSeq = 1;

        public Task<Apartment> Create(Apartment value)
        {
            value.Id = _idSeq++;
            _apartments.Add(value);
            return Task.FromResult(value);
        }

        public Task<Apartment?> Read(int id)
        {
            return Task.FromResult(_apartments.FirstOrDefault(a => a.Id == id));
        }

        public Task<Apartment> Update(Apartment oldVal, Apartment newVal)
        {
            var index = _apartments.IndexOf(oldVal);
            _apartments[index] = newVal;
            return Task.FromResult(newVal);
        }

        public Task Delete(int id)
        {
            _apartments.RemoveAll(a => a.Id == id);
            return Task.CompletedTask;
        }

        public Task<List<Apartment>> GetAll()
        {
            return Task.FromResult(_apartments.ToList());
        }
    }

    [Fact]
    public async Task AddApartment_ShouldAssignId_AndReturnApartment()
    {
        var repo = new InMemoryApartmentRepository();
        var service = new ApartmentService(repo);

        var apartment = new Apartment
        {
            Title = "Test",
            PricePerDay = 1000,
            OwnerId = 1
        };

        var created = await service.AddApartment(apartment);

        created.Id.Should().BeGreaterThan(0);
        (await service.GetAllApartments()).Should().ContainSingle();
    }

    [Fact]
    public async Task UpdateApartment_ShouldUpdateExistingApartment()
    {
        var repo = new InMemoryApartmentRepository();
        var service = new ApartmentService(repo);

        var created = await service.AddApartment(new Apartment
        {
            Title = "Old",
            Description = "Old desc",
            PricePerDay = 1000,
            OwnerId = 1
        });

        var request = new UpdateApartmentRequest
        {
            Id = created.Id,
            Title = "New",
            PricePerDay = 2000
        };

        var updated = await service.UpdateApartment(request);

        updated.Title.Should().Be("New");
        updated.PricePerDay.Should().Be(2000);
        updated.Description.Should().Be("Old desc");
    }

    [Fact]
    public async Task UpdateApartment_ShouldThrow_WhenNotFound()
    {
        var repo = new InMemoryApartmentRepository();
        var service = new ApartmentService(repo);

        var request = new UpdateApartmentRequest
        {
            Id = 999,
            Title = "New"
        };

        var act = async () => await service.UpdateApartment(request);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Apartment with id '999' not found");
    }
}

