namespace publisher_subcriber_ibckfv;

public class TransportAction
{
    public TransportActionType Type { get; }
    public string Description { get; }
    public DateTime DateAndTime { get; } = DateTime.Now;

    public TransportAction(TransportActionType type, string description)
    {
        Type = type;
        Description = description;
    }
}