namespace ReflectionTest;

public class LogAllHandler : IEventHandler<UserEnteredEvent>, IEventHandler<AccessDeniedEvent>
{
    public void Handle(UserEnteredEvent ev)
    {
        File.WriteAllText("logs.txt",
            $"UserEntered : role - {ev.Role}, name - {ev.Username}, time - {ev.Time}");
    }

    public void Handle(AccessDeniedEvent ev)
    {
        File.WriteAllText("logs.txt",
            $"AccessDenied :  name - {ev.Username}, reason - {ev.Reason}");
    }
}