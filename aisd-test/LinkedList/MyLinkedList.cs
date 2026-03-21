namespace LinkedList;

public class MyLinkedList<T>
{
    public Node<T> Head;
    private int _count;

    public int Count => _count;

    public bool IsEmpty() => Head == null;

    // Добавление в конец
    public void AddLast(T value)
    {
        var newNode = new Node<T>(value);

        if (Head == null)
        {
            Head = newNode;
        }
        else
        {
            Node<T> current = Head;
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
            Next = Head
        };
        Head = newNode;
        _count++;
    }

    // Удаление первого вхождения
    public bool Remove(T value)
    {
        if (Head == null) return false;

        if (Equals(Head.Value, value))
        {
            Head = Head.Next;
            _count--;
            return true;
        }

        Node<T> current = Head;
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
        Node<T> current = Head;
        while (current != null)
        {
            if (Equals(current.Value, value))
                return true;
            current = current.Next;
        }
        return false;
    }
}