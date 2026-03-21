namespace ReflectionTest;

public interface IEventHandler<T> where T : IEvent
{
    void Handle(T ev);
}