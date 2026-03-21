namespace publisher_subcriber_ibckfv;

public interface IObserver<T>
{
    public void Update(T info);
}