namespace ReflectionTest;

public class SecurityNotificationHandler : IOrderedEventHandler<AccessDeniedEvent>,
    IOrderedEventHandler<UserEnteredEvent>
{
    public int Order { get; } = 1;
    
    public void Handle(AccessDeniedEvent ev)
    {
        File.WriteAllText("logs.txt",
            $"AccessDenied :  name - {ev.Username}, reason - {ev.Reason}");
    }

    public bool ShouldProcess(AccessDeniedEvent ev)
    {
        return true;
    }

    public bool ShouldProcess(UserEnteredEvent ev)
    {
        return ev.Role == Role.Visitor;
    }

    public void Handle(UserEnteredEvent ev)
    {
        throw new NotImplementedException();
    }
}