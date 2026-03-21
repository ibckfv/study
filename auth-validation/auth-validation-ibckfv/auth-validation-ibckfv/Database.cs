using System.Security.Cryptography;
using auth_validation_ibckfv.models;
using Npgsql;

namespace auth_validation_ibckfv;

public class Database : IDisposable
{
    private string _connString;
    
    public Database(string connectionString)
    {
        _connString = connectionString;
    }

    public async Task<int> CreateUserWithHashAsync(string username, string email,
        string firstName, string lastName, string? phone, DateTime? dob, string hash, string salt, string role = "user")
    {
        const string sql = @"INSERT INTO users (username, email, first_name, last_name, phone, dob, password_hash, password_salt, role)
                             VALUES (@u,@e,@fn,@ln,@p,@dob,@ph,@ps,@role)
                             RETURNING user_id;";
        await using var conn = new NpgsqlConnection(_connString);
        await conn.OpenAsync();
        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("u", username);
        cmd.Parameters.AddWithValue("e", email);
        cmd.Parameters.AddWithValue("fn", firstName);
        cmd.Parameters.AddWithValue("ln", lastName);
        cmd.Parameters.AddWithValue("p", (object?)phone ?? DBNull.Value);
        cmd.Parameters.AddWithValue("dob", (object?)dob ?? DBNull.Value);
        cmd.Parameters.AddWithValue("ph", hash);
        cmd.Parameters.AddWithValue("ps", salt);
        cmd.Parameters.AddWithValue("role", role);
        var res = await cmd.ExecuteScalarAsync();
        return Convert.ToInt32(res);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        const string sql = @"SELECT user_id, username, email, first_name, last_name, phone, dob, password_hash, password_salt, role, created_at
                             FROM users WHERE username = @u LIMIT 1;";
        await using var conn = new NpgsqlConnection(_connString);
        await conn.OpenAsync();
        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("u", username);
        await using var r = await cmd.ExecuteReaderAsync();
        if (await r.ReadAsync())
        {
            return new User {
                UserId = r.GetInt32(0),
                Username = r.GetString(1),
                Email = r.GetString(2),
                FirstName = r.GetString(3),
                LastName = r.GetString(4),
                Phone = r.IsDBNull(5) ? null : r.GetString(5),
                Dob = r.IsDBNull(6) ? (DateTime?)null : r.GetDateTime(6),
                PasswordHash = r.GetString(7),
                PasswordSalt = r.GetString(8),
                Role = r.GetString(9),
                CreatedAt = r.GetDateTime(10)
            };
        }
        return null;
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        const string sql = @"SELECT user_id, username, email, first_name, last_name, phone, dob, password_hash, password_salt, role, created_at
                             FROM users WHERE email = @e LIMIT 1;";
        await using var conn = new NpgsqlConnection(_connString);
        await conn.OpenAsync();
        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("e", email);
        await using var r = await cmd.ExecuteReaderAsync();
        if (await r.ReadAsync())
        {
            return new User {
                UserId = r.GetInt32(0),
                Username = r.GetString(1),
                Email = r.GetString(2),
                FirstName = r.GetString(3),
                LastName = r.GetString(4),
                Phone = r.IsDBNull(5) ? null : r.GetString(5),
                Dob = r.IsDBNull(6) ? (DateTime?)null : r.GetDateTime(6),
                PasswordHash = r.GetString(7),
                PasswordSalt = r.GetString(8),
                Role = r.GetString(9),
                CreatedAt = r.GetDateTime(10)
            };
        }
        return null;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        const string sql = @"SELECT user_id, username, email, first_name, last_name, phone, dob, password_hash, password_salt, role, created_at
                             FROM users WHERE user_id = @id LIMIT 1;";
        await using var conn = new NpgsqlConnection(_connString);
        await conn.OpenAsync();
        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("id", id);
        await using var r = await cmd.ExecuteReaderAsync();
        if (await r.ReadAsync())
        {
            return new User {
                UserId = r.GetInt32(0),
                Username = r.GetString(1),
                Email = r.GetString(2),
                FirstName = r.GetString(3),
                LastName = r.GetString(4),
                Phone = r.IsDBNull(5) ? null : r.GetString(5),
                Dob = r.IsDBNull(6) ? (DateTime?)null : r.GetDateTime(6),
                PasswordHash = r.GetString(7),
                PasswordSalt = r.GetString(8),
                Role = r.GetString(9),
                CreatedAt = r.GetDateTime(10)
            };
        }
        return null;
    }

    // ADS
    public async Task<int> CreateAdAsync(string title, string? description, decimal? price, int userId)
    {
        const string sql = @"INSERT INTO ads (title, description, price, user_id) VALUES (@t,@d,@p,@uid) RETURNING ad_id;";
        await using var conn = new NpgsqlConnection(_connString);
        await conn.OpenAsync();
        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("t", title);
        cmd.Parameters.AddWithValue("d", (object?)description ?? DBNull.Value);
        cmd.Parameters.AddWithValue("p", (object?)price ?? DBNull.Value);
        cmd.Parameters.AddWithValue("uid", userId);
        var res = await cmd.ExecuteScalarAsync();
        return Convert.ToInt32(res);
    }

    public async Task<List<Ad>> GetAllAdsAsync()
    {
        var list = new List<Ad>();
        const string sql = "SELECT ad_id, title, description, price, created_at, user_id FROM ads ORDER BY ad_id DESC;";
        await using var conn = new NpgsqlConnection(_connString);
        await conn.OpenAsync();
        await using var cmd = new NpgsqlCommand(sql, conn);
        await using var r = await cmd.ExecuteReaderAsync();
        while (await r.ReadAsync())
        {
            list.Add(new Ad {
                AdId = r.GetInt32(0),
                Title = r.IsDBNull(1) ? null : r.GetString(1),
                Description = r.IsDBNull(2) ? null : r.GetString(2),
                Price = r.IsDBNull(3) ? null : r.GetDecimal(3),
                CreatedAt = r.GetDateTime(4),
                UserId = r.GetInt32(5)
            });
        }
        return list;
    }

    public void Dispose() { }
}