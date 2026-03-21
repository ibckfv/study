namespace chain_of_responsibility_ibckfv;

public class Application
{
    public Package Package { get; set; }
    public Guid Id { get; set; }
    public string Sender { get; set; }
    public string Recipient { get; set; }

    public Application(Package package, string sender, string recipient)
    {
        Id = Guid.NewGuid();
        this.Package = package;
        Sender = sender;
        Recipient = recipient;
    }
}