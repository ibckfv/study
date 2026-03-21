namespace chain_of_responsibility_ibckfv;

public class CarrierHandler : BaseHandler<Application>
{
    private string _startingPoint;
    private string _endingPoint;

    public CarrierHandler(string StartingPoint, string EndingPoint)
    {
        _startingPoint = StartingPoint;
        _endingPoint = EndingPoint;
    }
    public override IHandler<Application>? Successor { get; set; }

    public override void Handle(Application application)
    {
        Console.WriteLine($"Package - {application.Package.Name} delivered from {_startingPoint} to {_endingPoint} successfully");
        base.Handle(application);
    }
}