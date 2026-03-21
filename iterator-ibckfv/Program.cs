using iterator_ibckfv;

var parkingSpaces = new ParkingSpacesCollection();

parkingSpaces.Add(new ParkingSpace("Yes", "Yes", "No", 15, 200));
parkingSpaces.Add(new ParkingSpace("No", "No", "No", 90, 100));
parkingSpaces.Add(new ParkingSpace("No", "Yes", "No", 80, 500));
parkingSpaces.Add(new ParkingSpace("No", "No", "Yes", 10, 50));
parkingSpaces.Add(new ParkingSpace("Yes", "Yes", "Yes", 25, 1000));

foreach (var space in parkingSpaces)
{
    Console.WriteLine(space);
}
