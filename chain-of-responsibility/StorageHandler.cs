namespace chain_of_responsibility_ibckfv;

public class StorageHandler : BaseHandler<Application>
{
    public override IHandler<Application>? Successor { get; set; }

    public override void Handle(Application application)
    {
        Console.WriteLine($"Package - {application.Package.Name} was sent to the carrier company");
        base.Handle(application);
    }
}