using System.Reflection;

namespace ReflectionTest;

public class EventBus
{
    private List<object?> _handlers;

    public EventBus()
    {
        _handlers = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => !t.IsInterface)
            .Select(i => Activator.CreateInstance(i))
            .Where(e => e != null && e.GetType().GetInterfaces()
                .Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEventHandler<>)))
            .ToList();
    }

    public void Publish<T>(T ev) where T : IEvent
    {
        foreach (var handler in _handlers
                     .Where(h => h is IEventHandler<T>)
                     .Where(s => s is not IEventFilter<T> filter || filter.ShouldProcess(ev))
                     .OrderBy(ord => ord is IOrderedEventHandler<T> ordered ? ordered.Order : 999))
        {
            try
            {
                (handler as IEventHandler<T>).Handle(ev);
            }
            catch (Exception ex)
            {
                CatchError($"Error : {handler.GetType().Name} - {ex.Message}");
            }
        }
    }

    public void CatchError(string mess)
    {
        File.WriteAllText("logs.txt", mess);
    }
}