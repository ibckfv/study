namespace auth_validation_ibckfv.models;

public class Ad
{
    public int AdId { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
}