using ApartmentsBooking.Abstractions;
using ApartmentsBooking.Data;
using ApartmentsBooking.Models;
using ApartmentsBooking.RequestEntities;

namespace ApartmentsBooking.Services;

public class ApartmentService : IApartmentService
{
    private readonly IRepostiory<Apartment> _repository;
    private readonly ILogger<ApartmentService> _logger;

    public ApartmentService(IRepostiory<Apartment> apartmentRepository, ILogger<ApartmentService> logger)
    {
        _logger = logger;
        _repository = apartmentRepository;
    }
    public async Task<Apartment> AddApartment(Apartment apart)
    {
        await _repository.CreateAsync(apart);
        return apart;
    }

    public async Task<List<Apartment>> GetAllApartments()
    {
        _logger.LogDebug("Получение списка всех объявлений");
        var a = await _repository.GetAllAsync();
        return a.ToList();
    }

    public async Task<Apartment> UpdateApartment(UpdateApartmentRequest req)
    {
        var oldApartment = await _repository.GetAsync(req.Id);
        if (oldApartment == null)
        {
            throw new InvalidOperationException($"Apartment with id '{req.Id}' not found");
        }

        var newApartment = new Apartment
        {
            Id = oldApartment.Id,
            Title = req.Title ?? oldApartment.Title,
            Description = req.Description ?? oldApartment.Description,
            PricePerDay = req.PricePerDay ?? oldApartment.PricePerDay,
            OwnerId = oldApartment.OwnerId,
            Floor = req.Floor ?? oldApartment.Floor,
            TotalArea = req.TotalArea ?? oldApartment.TotalArea,
            Rooms = req.Rooms ?? oldApartment.Rooms,
            CreatedAt = oldApartment.CreatedAt
        };

        await _repository.UpdateAsync(newApartment);
        return newApartment;
    }

    public async Task DeleteApartment(Apartment apart)
    {
        await _repository.DeleteAsync((int)apart.Id);
    }
}