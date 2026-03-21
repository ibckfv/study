namespace ReflectionTest;

public class AccessDeniedEvent : IEvent
{
    public string Username { get; set; }
    public string Reason { get; set; }
}