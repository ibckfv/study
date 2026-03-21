namespace ApartmentsBooking.RequestEntities;

public class UpdateApartmentRequest
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? PricePerDay { get; set; }
    public int? Floor { get; set; }
    public int? TotalArea { get; set; }
    public int? Rooms { get; set; }
}