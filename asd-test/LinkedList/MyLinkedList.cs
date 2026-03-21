namespace LinkedList;

public class MyLinkedList<T>
{
    private Node<T> _head;
    private int _count;

    public int Count => _count;

    public bool IsEmpty() => _head == null;

    // Добавление в конец
    public void AddLast(T value)
    {
        var newNode = new Node<T>(value);

        if (_head == null)
        {
            _head = newNode;
        }
        else
        {
            Node<T> current = _head;
            while (current.Next != null)
                current = current.Next;

            current.Next = newNode;
        }

        _count++;
    }

    // Добавление в начало
    public void AddFirst(T value)
    {
        var newNode = new Node<T>(value)
        {
            Next = _head
        };
        _head = newNode;
        _count++;
    }

    // Удаление первого вхождения
    public bool Remove(T value)
    {
        if (_head == null) return false;

        if (Equals(_head.Value, value))
        {
            _head = _head.Next;
            _count--;
            return true;
        }

        Node<T> current = _head;
        while (current.Next != null && !Equals(current.Next.Value, value))
            current = current.Next;

        if (current.Next == null)
            return false;

        current.Next = current.Next.Next;
        _count--;
        return true;
    }

    // Поиск
    public bool Contains(T value)
    {
        Node<T> current = _head;
        while (current != null)
        {
            if (Equals(current.Value, value))
                return true;
            current = current.Next;
        }
        return false;
    }
}