namespace ApartmentsBooking.Models;

public class Booking
{
    public int Id { get; set; }
    public int ApartmentId { get; set; }
    public int UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreatedAt { get; set; }
}