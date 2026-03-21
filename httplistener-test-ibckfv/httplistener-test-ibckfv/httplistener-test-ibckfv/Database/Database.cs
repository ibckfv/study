using Npgsql;

namespace httplistener_test_ibckfv.Database;

public class Database : IDisposable
{
    private readonly string _connString;

    public Database(string connString)
    {
        _connString = connString;
    }
    public NpgsqlConnection GetConnection() => new NpgsqlConnection(_connString);
    
    public void Dispose()
    {
    }
}