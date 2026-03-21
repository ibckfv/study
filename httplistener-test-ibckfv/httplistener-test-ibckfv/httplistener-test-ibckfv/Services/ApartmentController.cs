using httplistener_test_ibckfv.Dto;
using httplistener_test_ibckfv.models;
using httplistener_test_ibckfv.Repositories;

namespace httplistener_test_ibckfv.Services;

public class ApartmentController
{
    private readonly ApartmentRepository _aptRepo;
    private readonly CityRepository _cityRepo;

    public ApartmentController(ApartmentRepository aptRepo, CityRepository cityRepo)
    {
        _aptRepo = aptRepo;
        _cityRepo = cityRepo;
    }

    public Apartment CreateApartment(ApartmentCreateDto dto, int ownerId)
    {
        //валидацию не успел
        return _aptRepo.Create(dto, ownerId);
    }

    public IEnumerable<object> GetApartments(string? city, string? sort)
    {
        return _aptRepo.GetApartments(city, sort);
    }
}
