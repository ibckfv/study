namespace MyList;

public class MyList<T>
{
    private T[] _items;
    private int _count;
    private const int DefaultCapacity = 4;

    public MyList()
    {
        _items = new T[DefaultCapacity];
        _count = 0;
    }

    public int Count => _count;

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
                throw new ArgumentOutOfRangeException();
            return _items[index];
        }
        set
        {
            if (index < 0 || index >= _count)
                throw new ArgumentOutOfRangeException();
            _items[index] = value;
        }
    }

    public void Add(T item)
    {
        EnsureCapacity();
        _items[_count++] = item;
    }

    public bool Remove(T item)
    {
        int index = IndexOf(item);
        if (index == -1)
            return false;

        for (int i = index; i < _count - 1; i++)
            _items[i] = _items[i + 1];

        _items[_count - 1] = default;
        _count--;
        return true;
    }

    public int IndexOf(T item)
    {
        EqualityComparer<T> comparer = EqualityComparer<T>.Default;
        for (int i = 0; i < _count; i++)
        {
            if (comparer.Equals(_items[i], item))
                return i;
        }
        return -1;
    }

    private void EnsureCapacity()
    {
        if (_count >= _items.Length)
        {
            int newCapacity = _items.Length * 2;
            T[] newArray = new T[newCapacity];
            Array.Copy(_items, newArray, _items.Length);
            _items = newArray;
        }
    }

    public void Clear()
    {
        _items = new T[DefaultCapacity];
        _count = 0;
    }
}