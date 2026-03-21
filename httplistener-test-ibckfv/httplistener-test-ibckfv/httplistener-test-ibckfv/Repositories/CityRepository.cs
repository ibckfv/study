namespace httplistener_test_ibckfv.Repositories;

public class CityRepository
{
    private readonly Database.Database _db;

    public CityRepository(Database.Database db)
    {
        _db = db;
    }

    public int GetOrCreateCityId(string cityName)
    {
        if (string.IsNullOrWhiteSpace(cityName)) throw new ArgumentException("City name required");
        using var c = _db.GetConnection();
        c.Open();
        using (var cmd = c.CreateCommand())
        {
            cmd.CommandText = "SELECT id FROM cities WHERE lower(name) = lower(@name)";
            cmd.Parameters.AddWithValue("name", cityName);
            var r = cmd.ExecuteScalar();
            if (r != null && r != DBNull.Value)
            {
                return (int)r;
            }
        }
        var formatted = char.ToUpper(cityName[0]) + (cityName.Length>1 ? cityName.Substring(1).ToLower() : "");
        using (var cmd = c.CreateCommand())
        {
            cmd.CommandText = "INSERT INTO cities (name) VALUES (@name) RETURNING id";
            cmd.Parameters.AddWithValue("name", formatted);
            return (int)cmd.ExecuteScalar()!;
        }
    }

    public int? FindCityIdByName(string cityName)
    {
        if (string.IsNullOrWhiteSpace(cityName))
        {
            return null;
        }
        using var c = _db.GetConnection();
        c.Open();
        using var cmd = c.CreateCommand();
        cmd.CommandText = "SELECT id FROM cities WHERE lower(name) = lower(@name)";
        cmd.Parameters.AddWithValue("name", cityName);
        var r = cmd.ExecuteScalar();
        if (r == null || r == DBNull.Value)
        {
            return null;
        }
        return (int)r;
    }
}