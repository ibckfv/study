using System.Collections;

namespace StreamsLinqHomework;

public class ParkingSpacesIterator : IEnumerator<ParkingSpace>
{
    private int _currentIndex = -1;
    private List<ParkingSpace>? _sortedSpaces;

    public ParkingSpacesIterator(List<ParkingSpace> parkingSpaces)
    {
        SortSpaces(parkingSpaces);
    }

    private void SortSpaces(List<ParkingSpace> parkingSpacesList)
    {
        _sortedSpaces = new List<ParkingSpace>(parkingSpacesList);

        for (int i = 0; i < _sortedSpaces.Count; i++)
        {
            for (int j = 0; j < _sortedSpaces.Count - i - 1; j++)
            {
                if (SpacePriorityCalculator.CalculatePriority(_sortedSpaces[j]) > 
                    SpacePriorityCalculator.CalculatePriority(_sortedSpaces[j + 1]))
                {
                    (_sortedSpaces[j], _sortedSpaces[j + 1]) = (_sortedSpaces[j + 1], _sortedSpaces[j]);
                }
            }
        }
    }

    public bool MoveNext()
    {
        _currentIndex++;
        return _currentIndex < _sortedSpaces.Count;
    }

    public void Reset()
    {
        _currentIndex = -1;
    }

    public ParkingSpace Current
    {
        get
        {
            if (_currentIndex < 0 || _currentIndex >= _sortedSpaces.Count || _sortedSpaces is null)
            {
                throw new InvalidOperationException("Index was outside the bounds of the array");
            }

            return _sortedSpaces[_currentIndex];
        }
    }

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        _sortedSpaces = null;
    }
}