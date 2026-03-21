namespace StreamsLinqHomework;

public class ParkingSpace
{
    public bool IsPayable { get; }
    public bool IsLegal { get; }
    public bool IsOccupied { get; }
    public double PopularityRating { get; }
    public double DistanceToUniversity { get; }

    public ParkingSpace(string isPayable, string isLegal, string isOccupied, double popularityRating, double distanceToUniversity)
    {
        IsPayable = isPayable == "Yes" ? true : false;
        IsLegal = isLegal == "Yes" ? true : false;
        IsOccupied = isOccupied == "Yes" ? true : false;
        PopularityRating = popularityRating;
        DistanceToUniversity = distanceToUniversity;
    }

    public override string ToString()
    {
        return $"{(IsLegal ? "Legal" : "Unlegal")}, {(IsPayable ? "payable" : "free")} " +
               $"parking place with {PopularityRating} rating and {DistanceToUniversity} " +
               $"meters from university is {(IsOccupied ? "occupied" : "available")}";
    }
}