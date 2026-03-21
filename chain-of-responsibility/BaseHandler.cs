namespace chain_of_responsibility_ibckfv;

public class BaseHandler<T> : IHandler<T>
{
    public virtual IHandler<T>? Successor { get; set; }
    
    public virtual void Handle(T package)
    {
        Successor?.Handle(package);
    }
}