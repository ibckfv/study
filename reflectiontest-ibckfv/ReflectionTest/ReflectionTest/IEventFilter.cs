namespace ReflectionTest;

public interface IEventFilter<T> where T : IEvent
{
    bool ShouldProcess(T ev);
}