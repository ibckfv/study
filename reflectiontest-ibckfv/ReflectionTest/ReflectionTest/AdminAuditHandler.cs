namespace ReflectionTest;

public class AdminAuditHandler : IOrderedEventHandler<UserEnteredEvent>
{
    public int Order { get; } = 0;
    
    public void Handle(UserEnteredEvent ev)
    {
        File.WriteAllText("logs.txt",
            $"AdminEntered : name - {ev.Username}, time - {ev.Time}");
    }

    public bool ShouldProcess(UserEnteredEvent ev)
    {
        return ev.Role == Role.Admin;
    }
}