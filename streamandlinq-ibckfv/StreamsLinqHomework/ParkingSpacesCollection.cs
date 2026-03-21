using System.Collections;

namespace StreamsLinqHomework;

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

    public void GetMinimumDistancedSpace()
    {
        Console.WriteLine(_parkingSpaces
            .Where(x => !x.IsOccupied)
            .OrderBy(x => x.DistanceToUniversity)
            .FirstOrDefault());
    }

    public List<ParkingSpace> CreateTopByRating()
    {
        return _parkingSpaces
            .OrderByDescending(x => x.PopularityRating)
            .Take(3)
            .ToList();
    }

    public string GetPayableStats()
    {
        return _parkingSpaces
            .GroupBy(ps => ps.IsPayable)
            .Select(g => new { Payable = g.Key, Count = g.Count() })
            .Aggregate(
                 "",
                 (result, item) =>
                     result + $"{(item.Payable ? "Payable" : "Free")}: {item.Count} places, ",
                 result => result.TrimEnd(',', ' ')
            );
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