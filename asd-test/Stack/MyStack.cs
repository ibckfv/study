namespace Stack;

public class MyStack<T>
{
    private T[] _items;
    private int _count;
    private const int DefaultCapacity = 4;

    public MyStack()
    {
        _items = new T[DefaultCapacity];
        _count = 0;
    }

    public int Count => _count;

    public bool IsEmpty() => _count == 0;

    public void Push(T item)
    {
        if (_count == _items.Length)
            Resize();

        _items[_count++] = item;
    }

    public T Pop()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Стек пуст.");

        T item = _items[--_count];
        _items[_count] = default; // очищаем ссылку
        return item;
    }

    public T Peek()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Стек пуст.");
        return _items[_count - 1];
    }

    private void Resize()
    {
        int newCapacity = _items.Length * 2;
        T[] newArray = new T[newCapacity];
        Array.Copy(_items, newArray, _items.Length);
        _items = newArray;
    }
}