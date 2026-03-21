namespace ApartmentsBooking.Models;

public class User
{
    public long Id { get; set; }
    public string? FullName { get; set; }
    public string? Phone { get; set; }

    public string? Login { get; set; }
    public string? Password { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<Apartment> Apartments { get; set; } = new();
    public List<Booking> Bookings { get; set; } = new();
}