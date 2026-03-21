namespace ApartmentsBooking.Models;

public class Booking
{
    public long Id { get; set; }
    public long? ApartmentId { get; set; }
    public Apartment? Apartment { get; set; }
    public long? UserId { get; set; }
    public User? Owner { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreatedAt { get; set; }
}