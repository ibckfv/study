namespace httplistener_test_ibckfv.models;

public class Review
{
    public int ReviewId { get; set; }
    public int ApartmentId { get; set; }
    public int UserId { get; set; }
    public string? Comment { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Rating { get; set; }
}