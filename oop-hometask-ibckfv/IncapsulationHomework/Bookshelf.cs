namespace IncapsulationHomework;

public class Bookshelf : IMeasurable
{
    public int Length { get; protected set; }
    public int Width { get; protected set; }

    public Bookshelf(int length, int width)
    {
        Length = length;
        Width = width;
    }

    public void CalculateArea()
    {
        Console.WriteLine($"Площадь составляет {Length * Width} сантиметров");
    }
}    
    