using System.Collections;

namespace iterator_ibckfv;

public class ParkingSpacesCollection : IEnumerable<ParkingSpace>
{
    private List<ParkingSpace> _parkingSpaces;

    public ParkingSpacesCollection()
    {
        _parkingSpaces = new List<ParkingSpace>();
    }
    
    public int Count()
    {
        return _parkingSpaces.Count;
    }

    public void Add(ParkingSpace item)
    {
        _parkingSpaces.Add(item);
    }

    public void Remove(ParkingSpace item)
    {
        _parkingSpaces.Remove(item);
    }

    public void Clear()
    {
        _parkingSpaces.Clear();
    }
    public IEnumerator<ParkingSpace> GetEnumerator()
    {
        return new ParkingSpacesIterator(_parkingSpaces);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}