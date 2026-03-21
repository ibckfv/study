using httplistener_test_ibckfv.models;
using httplistener_test_ibckfv.Database;

namespace httplistener_test_ibckfv.Repositories;

public class BookingRepository
{
    private readonly Database.Database _db;

    public BookingRepository(Database.Database db)
    {
        _db = db;
    }

    public Booking Create(Booking b)
    {
        using var c = _db.GetConnection();
        c.Open();
        using var cmd = c.CreateCommand();
        cmd.CommandText = "INSERT INTO bookings (apartment_id, user_id, date_start, date_end, status) VALUES (@ap,@u,@ds,@de,@st) RETURNING id";
        cmd.Parameters.AddWithValue("ap", b.ApartmentId);
        cmd.Parameters.AddWithValue("u", b.UserId);
        cmd.Parameters.AddWithValue("ds", b.DateStart.Date);
        cmd.Parameters.AddWithValue("de", b.DateEnd.Date);
        cmd.Parameters.AddWithValue("st", b.Status);
        b.Id = (int)cmd.ExecuteScalar()!;
        return b;
    }

    public Booking? GetById(int id)
    {
        using var c = _db.GetConnection();
        c.Open();
        using var cmd = c.CreateCommand();
        cmd.CommandText = "SELECT id, apartment_id, user_id, date_start, date_end, status FROM bookings WHERE id=@id";
        cmd.Parameters.AddWithValue("id", id);
        using var r = cmd.ExecuteReader();
        if (r.Read()) return new Booking {
            Id = r.GetInt32(0),
            ApartmentId = r.GetInt32(1),
            UserId = r.GetInt32(2),
            DateStart = r.GetDateTime(3),
            DateEnd = r.GetDateTime(4),
            Status = r.GetString(5)
        };
        return null;
    }

    public void UpdateStatus(int bookingId, string status)
    {
        using var c = _db.GetConnection();
        c.Open();
        using var cmd = c.CreateCommand();
        cmd.CommandText = "UPDATE bookings SET status=@s WHERE id=@id";
        cmd.Parameters.AddWithValue("s", status);
        cmd.Parameters.AddWithValue("id", bookingId);
        cmd.ExecuteNonQuery();
    }

    public bool ExistsOverlap(int apartmentId, DateTime start, DateTime end, string status)
    {
        using var c = _db.GetConnection();
        c.Open();
        using var cmd = c.CreateCommand();
        cmd.CommandText = @"SELECT COUNT(1) FROM bookings 
                WHERE apartment_id = @apartmentId 
                  AND status = @status 
                  AND NOT (date_end < @start OR date_start > @end)";
        cmd.Parameters.AddWithValue("apartmentId", apartmentId);
        cmd.Parameters.AddWithValue("status", status);
        cmd.Parameters.AddWithValue("start", start.Date);
        cmd.Parameters.AddWithValue("end", end.Date);
        var cnt = (long)cmd.ExecuteScalar()!;
        return cnt > 0;
    }

    public bool ExistsOverlapExcludingBooking(int apartmentId, DateTime start, DateTime end, string status, int excludeBookingId)
    {
        using var c = _db.GetConnection(); c.Open();
        using var cmd = c.CreateCommand();
        cmd.CommandText = @"SELECT COUNT(1) FROM bookings 
                WHERE apartment_id = @apartmentId 
                  AND status = @status 
                  AND id <> @exclude 
                  AND NOT (date_end < @start OR date_start > @end)";
        cmd.Parameters.AddWithValue("apartmentId", apartmentId);
        cmd.Parameters.AddWithValue("status", status);
        cmd.Parameters.AddWithValue("exclude", excludeBookingId);
        cmd.Parameters.AddWithValue("start", start.Date);
        cmd.Parameters.AddWithValue("end", end.Date);
        var cnt = (long)cmd.ExecuteScalar()!;
        return cnt > 0;
    }

    public IEnumerable<object> Top3ApartmentsByApprovedBookings()
    {
        using var c = _db.GetConnection(); c.Open();
        using var cmd = c.CreateCommand();
        cmd.CommandText = @"SELECT a.id, a.title, a.owner_id, COUNT(b.*) as approved_count
            FROM apartments a
            JOIN bookings b ON b.apartment_id = a.id
            WHERE b.status = 'Approved'
            GROUP BY a.id, a.title, a.owner_id
            ORDER BY COUNT(b.*) DESC
            LIMIT 3";
        using var rdr = cmd.ExecuteReader();
        var list = new List<object>();
        while (rdr.Read())
        {
            list.Add(new
            {
                id = rdr.GetInt32(0),
                title = rdr.GetString(1),
                ownerId = rdr.GetInt32(2),
                approvedCount = rdr.GetInt64(3)
            });
        }
        return list;
    }
}