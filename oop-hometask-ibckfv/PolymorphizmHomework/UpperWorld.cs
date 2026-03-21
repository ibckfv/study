namespace PolymorphizmHomework;

public class UpperWorld : MinecraftUniverse
{
    public string[] WorldInhabitants { get; set; }
    public string[] Resources { get;set; }

    public UpperWorld(string[] inhabitants, string[] resources) : base("Лесной биом", 30000, 30000)
    {
        WorldInhabitants = inhabitants;
        Resources = resources;
    }

    public void GetResourceUsage(int resourceNumber)
    {
        if (resourceNumber >= 0 && ((Resources.Length) >= resourceNumber))
        {
            Console.WriteLine($"Ресурс под названием {Resources[resourceNumber - 1]} применяется для ...");
        }
        else 
        { 
            throw new Exception("Элемента с таким номером не существует"); 
        }
    }

    public void GetResourceUsage(string resourceName)
    {
        if (Resources.Contains(resourceName))
        {
            Console.WriteLine($"Ресурс под названием {resourceName} применяется для ...");
        }
        else
        {
            throw new Exception("Такого элемента не существует");
        }
    }

    public override void GetDangerLevel()
    {
        Console.WriteLine($"Ураень опасности в {BiomName} равен 10");
    }

    public override void UniverseFeatures()
    {
        Console.WriteLine("Upper world - мир с дневным и ночным циклом, враждебными и дружелюбными мобами, " +
                          $"такими как {WorldInhabitants} и следующими полезными ископаемыми: {Resources}");
    }
}