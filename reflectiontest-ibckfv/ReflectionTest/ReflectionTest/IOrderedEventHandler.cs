namespace ReflectionTest;

public interface IOrderedEventHandler<T> : IEventHandler<T> where T : IEvent
{
    int Order { get; }
}