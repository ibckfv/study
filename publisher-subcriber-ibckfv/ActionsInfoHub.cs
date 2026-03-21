using System.Diagnostics;
using System.Threading.Channels;

namespace publisher_subcriber_ibckfv;

public class ActionsInfoHub : IObservable
{
    private List<TransportAction> _actionsList = new();
    public event Action<TransportAction> OnActionsUpdates = _ =>
    {
        Console.WriteLine($"New transport action was happen");
    }; 
    
    public void NotifyObservers()
    {
        try
        {
            OnActionsUpdates(_actionsList[^1]);
        }
        catch
        {
            Console.WriteLine($"No actions to notify!");
        }
    }

    public void UpdateInformation(TransportAction action)
    {
        _actionsList.Add(action);
        NotifyObservers();
    }
}