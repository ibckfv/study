namespace Dictionary;

public class MyDictionary<TKey, TValue>
{
    private const int DefaultCapacity = 8;
    private const float LoadFactor = 0.7f;

    private Entry[] _entries;
    private int _count;

    private class Entry
    {
        public TKey Key;
        public TValue Value;
        public bool IsDeleted;

        public Entry(TKey key, TValue value)
        {
            Key = key;
            Value = value;
            IsDeleted = false;
        }
    }

    public MyDictionary()
    {
        _entries = new Entry[DefaultCapacity];
        _count = 0;
    }

    private int GetIndex(TKey key, int attempt = 0)
    {
        int hash = (key.GetHashCode() & 0x7FFFFFFF);
        return (hash + attempt) % _entries.Length;
    }

    public int Count => _count;

    public void Add(TKey key, TValue value)
    {
        if ((_count + 1.0) / _entries.Length > LoadFactor)
            Resize();

        for (int i = 0; i < _entries.Length; i++)
        {
            int index = GetIndex(key, i);

            if (_entries[index] == null || _entries[index].IsDeleted)
            {
                _entries[index] = new Entry(key, value);
                _count++;
                return;
            }

            if (!_entries[index].IsDeleted && _entries[index].Key.Equals(key))
                throw new ArgumentException("Ключ уже существует.");
        }

        throw new InvalidOperationException("Словарь переполнен.");
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        for (int i = 0; i < _entries.Length; i++)
        {
            int index = GetIndex(key, i);
            var entry = _entries[index];

            if (entry == null)
                break;

            if (!entry.IsDeleted && entry.Key.Equals(key))
            {
                value = entry.Value;
                return true;
            }
        }

        value = default;
        return false;
    }

    public bool Remove(TKey key)
    {
        for (int i = 0; i < _entries.Length; i++)
        {
            int index = GetIndex(key, i);
            var entry = _entries[index];

            if (entry == null)
                break;

            if (!entry.IsDeleted && entry.Key.Equals(key))
            {
                entry.IsDeleted = true;
                _count--;
                return true;
            }
        }
        return false;
    }

    private void Resize()
    {
        var oldEntries = _entries;
        _entries = new Entry[oldEntries.Length * 2];
        _count = 0;

        foreach (var entry in oldEntries)
        {
            if (entry != null && !entry.IsDeleted)
                Add(entry.Key, entry.Value);
        }
    }
}