namespace PolymorphizmHomework;

public class Bookshelf : IMeasurable
{
    public int Length { get; set; }
    public int Width { get; set; }

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
    