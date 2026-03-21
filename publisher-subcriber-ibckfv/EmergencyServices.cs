namespace publisher_subcriber_ibckfv;

public class EmergencyServices : IObserver<TransportAction>
{
    public void Update(TransportAction info)
    {
        Console.WriteLine($"Emergency services are notified about {info.Type}, at {DateTime.Now}");
    }
}