namespace chain_of_responsibility_ibckfv;

public interface IHandler<T>
{
    public IHandler<T>? Successor { get; set; }
    public void Handle(T package);
}