namespace ReflectionTest;

public class UserEnteredEvent : IEvent
{
    public string Username { get; set; }
    public Role Role { get; set; }
    public DateTime Time { get; set; } = DateTime.Now;
}