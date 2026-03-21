namespace ApartmentsBooking.RequestEntities;

public class UpdateUserRequest
{
    public string Login { get; set; } = "";
    public string? FullName { get; set; }
    public string? Phone { get; set; }
}