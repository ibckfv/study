namespace publisher_subcriber_ibckfv;

public class MobileMaps : IObserver<TransportAction>
{
    public void Update(TransportAction info)
    {
        Console.WriteLine($"{info.DateAndTime} : {info.Type} details - {info.Description}");
    }
}