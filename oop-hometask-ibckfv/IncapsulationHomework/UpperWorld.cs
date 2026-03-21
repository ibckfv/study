namespace IncapsulationHomework;

public class UpperWorld : MinecraftUniverse
{
    private string[] _worldInhabitants;
    private string[] _resources;

    public UpperWorld(string[] inhabitants, string[] resources) : base("Лесной биом", 30000, 30000)
    {
        _worldInhabitants = inhabitants;
        _resources = resources;
    }

    public void GetResourceUsage(int resourceNumber)
    {
        if (resourceNumber >= 0 && ((_resources.Length) >= resourceNumber))
        {
            Console.WriteLine($"Ресурс под названием {_resources[resourceNumber - 1]} применяется для ...");
        }
        else 
        { 
            throw new Exception("Элемента с таким номером не существует"); 
        }
    }

    public void GetResourceUsage(string resourceName)
    {
        if (_resources.Contains(resourceName))
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
                          $"такими как {_worldInhabitants} и следующими полезными ископаемыми: {_resources}");
    }
}