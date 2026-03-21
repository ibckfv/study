namespace chain_of_responsibility_ibckfv;

public class LogisticsHandler : BaseHandler<Application>
{
    private List<string> _partnersList;

    public LogisticsHandler(List<string> PartnersList)
    {
        _partnersList = PartnersList;
    }
    public override IHandler<Application>? Successor { get; set; }

    public override void Handle(Application application)
    {
        Console.WriteLine($"Package - {application.Package.Name} was passed to {_partnersList[new Random().Next(_partnersList.Count - 1)]}");
        base.Handle(application);
    }
}