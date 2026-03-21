namespace Queue;

public class MyQueue<T>
{
    private T[] _items;
    private int _head;  // индекс начала очереди
    private int _tail;  // индекс конца очереди
    private int _count; // количество элементов
    private const int DefaultCapacity = 4;

    public MyQueue()
    {
        _items = new T[DefaultCapacity];
        _head = 0;
        _tail = 0;
        _count = 0;
    }

    public int Count => _count;

    public bool IsEmpty() => _count == 0;

    public void Enqueue(T item)
    {
        if (_count == _items.Length)
            Resize();

        _items[_tail] = item;
        _tail = (_tail + 1) % _items.Length;
        _count++;
    }

    public T Dequeue()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Очередь пуста.");

        T item = _items[_head];
        _items[_head] = default; // очищаем ссылку
        _head = (_head + 1) % _items.Length;
        _count--;
        return item;
    }

    public T Peek()
    {
        if (IsEmpty())
            throw new InvalidOperationException("Очередь пуста.");
        return _items[_head];
    }

    private void Resize()
    {
        int newCapacity = _items.Length * 2;
        T[] newArray = new T[newCapacity];

        for (int i = 0; i < _count; i++)
            newArray[i] = _items[(_head + i) % _items.Length];

        _items = newArray;
        _head = 0;
        _tail = _count;
    }
}