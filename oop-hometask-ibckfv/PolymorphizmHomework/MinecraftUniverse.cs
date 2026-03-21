namespace PolymorphizmHomework;

public abstract class MinecraftUniverse : IMeasurable
{
    public string BiomName { get; set; }
    public int Length { get; set; }
    public int Width {  get; set; }

    public MinecraftUniverse(string biomName, int length, int width)
    {
        BiomName = biomName;
        Length = length;
        Width = width;
    }

    public void CalculateArea()
    {
        Console.WriteLine($"Площадь составляет {Length * Width} блоков");
    }

    public virtual void GetDangerLevel()
    {
        Console.WriteLine($"Уровень опастности в {BiomName} неизвестен");
    }

    public abstract void UniverseFeatures();
}