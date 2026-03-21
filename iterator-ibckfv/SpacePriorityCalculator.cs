namespace iterator_ibckfv;

public class SpacePriorityCalculator
{
    private ParkingSpace _space;

    public SpacePriorityCalculator(ParkingSpace space)
    {
        _space = space;
    }
    
    public static double CalculatePriority(ParkingSpace space)
    {
        var priorityCoefficent = 1.0 + (1.0 / (space.PopularityRating / 100.0)) + (1.0 / (space.DistanceToUniversity / 100.0));
        
        if (!space.IsOccupied)
        {
            priorityCoefficent += 2;
        }

        if (space.IsLegal)
        {
            priorityCoefficent += 2;
        }

        if (!space.IsPayable)
        {
            priorityCoefficent += 2;
        }

        return priorityCoefficent;
    }
}