using httplistener_test_ibckfv.Dto;
using httplistener_test_ibckfv.models;

namespace httplistener_test_ibckfv.Repositories;

public class ApartmentRepository
{
    private readonly Database.Database _db;
    private readonly CityRepository _cities;

    public ApartmentRepository(Database.Database db, CityRepository cities)
    {
        _db = db;
        _cities = cities;
    }

    public Apartment Create(ApartmentCreateDto dto, int ownerId)
    {
        var cityId = _cities.GetOrCreateCityId(dto.City);
        using var c = _db.GetConnection();
        c.Open();
        using var cmd = c.CreateCommand();
        cmd.CommandText = @"INSERT INTO apartments (title, description, price_per_day, owner_id, city_id)
                              VALUES (@t,@d,@p,@o,@city) RETURNING id, created_at";
        cmd.Parameters.AddWithValue("t", dto.Title);
        cmd.Parameters.AddWithValue("d", dto.Description ?? string.Empty);
        cmd.Parameters.AddWithValue("p", dto.PricePerDay);
        cmd.Parameters.AddWithValue("o", ownerId);
        cmd.Parameters.AddWithValue("city", cityId);
        using var r = cmd.ExecuteReader();
        if (r.Read())
        {
            return new Apartment
            {
                Id = r.GetInt32(0),
                Title = dto.Title,
                Description = dto.Description,
                PricePerDay = dto.PricePerDay,
                OwnerId = ownerId, CityId = cityId,
                CreatedAt = r.GetDateTime(1)
            };
        }
        throw new Exception("Failed to create apartment");
    }

    public IEnumerable<object> GetApartments(string? cityFilter, string? sort)
    {
        using var c = _db.GetConnection();
        c.Open();
        using var cmd = c.CreateCommand();
        var order = (sort ?? "").ToLower() == "desc" ? "DESC" : "ASC";
        cmd.CommandText = $@"SELECT a.id, a.title, a.description, a.price_per_day, a.owner_id, c.name as city, a.created_at
            FROM apartments a
            JOIN cities c ON c.id = a.city_id
            WHERE (@city IS NULL OR lower(c.name) = lower(@city))
            ORDER BY
                CASE WHEN NULLIF(trim(a.price_per_day),'') IS NULL THEN NULL ELSE (regexp_replace(a.price_per_day, '[^0-9\.\,]', '', 'g'))::numeric END {order}
        ";
        cmd.Parameters.AddWithValue("city", (object?)cityFilter ?? DBNull.Value);
        using var rdr = cmd.ExecuteReader();
        var list = new List<object>();
        while (rdr.Read())
        {
            list.Add(new {
                id = rdr.GetInt32(0),
                title = rdr.GetString(1),
                description = rdr.IsDBNull(2)? null : rdr.GetString(2),
                pricePerDay = rdr.GetString(3),
                ownerId = rdr.GetInt32(4),
                city = rdr.GetString(5),
                createdAt = rdr.GetDateTime(6)
            });
        }
        return list;
    }

    public Apartment? GetById(int id)
    {
        using var c = _db.GetConnection();
        c.Open();
        using var cmd = c.CreateCommand();
        cmd.CommandText = "SELECT id, title, description, price_per_day, owner_id, city_id, created_at FROM apartments WHERE id=@id";
        cmd.Parameters.AddWithValue("id", id);
        using var r = cmd.ExecuteReader();
        if (r.Read()) return new Apartment {
            Id = r.GetInt32(0),
            Title = r.GetString(1),
            Description = r.IsDBNull(2)?null:r.GetString(2),
            PricePerDay = r.GetString(3),
            OwnerId = r.GetInt32(4),
            CityId = r.GetInt32(5),
            CreatedAt = r.GetDateTime(6)
        };
        return null;
    }
}