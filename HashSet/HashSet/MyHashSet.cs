namespace HashSet;

public class MyHashSet<T>
{
    private const int InitialCapacity = 16;
    private const float LoadFactor = 0.75f;

    private LinkedList<T>[] buckets;
    private int count;

    public MyHashSet()
    {
        buckets = new LinkedList<T>[InitialCapacity];
    }

    private int GetBucketIndex(T item)
    {
        int hash = item == null ? 0 : item.GetHashCode();
        return Math.Abs(hash) % buckets.Length;
    }

    public bool Add(T item)
    {
        if (Contains(item))
        {
            return false;
        }

        if ((float)(count + 1) / buckets.Length > LoadFactor)
        {
            Resize();
        }

        int index = GetBucketIndex(item);
        if (buckets[index] == null)
        {
            buckets[index] = new LinkedList<T>();
        }

        buckets[index].AddLast(item);
        count++;
        return true;
    }

    public bool Contains(T item)
    {
        int index = GetBucketIndex(item);
        if (buckets[index] == null)
        {
            return false;
        }

        foreach (T element in buckets[index])
        {
            if (EqualityComparer<T>.Default.Equals(element, item))
            {
                return true;
            }
        }

        return false;
    }

    public bool Remove(T item)
    {
        int index = GetBucketIndex(item);
        if (buckets[index] == null)
        {
            return false;
        }

        LinkedListNode<T> current = buckets[index].First;
        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Value, item))
            {
                buckets[index].Remove(current);
                count--;
                return true;
            }
            current = current.Next;
        }

        return false;
    }

    private void Resize()
    {
        var newBuckets = new LinkedList<T>[buckets.Length * 2];
        foreach (var bucket in buckets)
        {
            if (bucket == null)
            {
                continue;
            }

            foreach (var item in bucket)
            {
                int newIndex = Math.Abs(item.GetHashCode()) % newBuckets.Length;
                if (newBuckets[newIndex] == null)
                {
                    newBuckets[newIndex] = new LinkedList<T>();
                }
                newBuckets[newIndex].AddLast(item);
            }
        }

        buckets = newBuckets;
    }

    public int Count => count;
}