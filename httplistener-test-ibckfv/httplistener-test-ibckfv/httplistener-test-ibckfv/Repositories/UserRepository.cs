using httplistener_test_ibckfv.models;

namespace httplistener_test_ibckfv.Repositories;

public class UserRepository
{
    private readonly Database.Database _db;

    public UserRepository(Database.Database db)
    {
        _db = db;
    }
    
    public User? FindByLogin(string login)
    {
        using var c = _db.GetConnection();
        c.Open();
        using var cmd = c.CreateCommand();
        cmd.CommandText = "SELECT id, login, password_hash, full_name, role FROM users WHERE login = @login";
        cmd.Parameters.AddWithValue("login", login);
        using var r = cmd.ExecuteReader();
        if (r.Read())
        {
            return new User
            {
                Id = r.GetInt32(0),
                Login = r.GetString(1),
                PasswordHash = r.GetString(2),
                FullName = r.IsDBNull(3) ? null : r.GetString(3),
                Role = r.GetString(4)
            };
        }
        return null;
    }
    public User? GetById(int id)
    {
        using var c = _db.GetConnection();
        c.Open();
        using var cmd = c.CreateCommand();
        cmd.CommandText = "SELECT id, login, password_hash, full_name, role FROM users WHERE id = @id";
        cmd.Parameters.AddWithValue("id", id);
        using var r = cmd.ExecuteReader();
        if (r.Read()) 
        {
            return new User 
            {
                Id = r.GetInt32(0),
                Login = r.GetString(1),
                PasswordHash = r.GetString(2),
                FullName = r.IsDBNull(3) ? null : r.GetString(3),
                Role = r.GetString(4)
            };
        }
        return null;
    }
}