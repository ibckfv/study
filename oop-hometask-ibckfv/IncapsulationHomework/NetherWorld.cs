namespace IncapsulationHomework;

public class NetherWorld : MinecraftUniverse
{
    private string[] _worldInhabitants;
    private string[] _resources;

    public NetherWorld(string[] inhabitants, string[] resources) : base("Лавовые озёра", 3000, 3000)
    {
        _worldInhabitants = inhabitants;
        _resources = resources;
    }

    public override void GetDangerLevel()
    {
        Console.WriteLine($"Уровень опасности в {BiomName} равен 100");
    }

    public override void UniverseFeatures()
    {
        Console.WriteLine("Nether world - мир ада с исключительно враждебными мобами, " +
                          $"такими как {_worldInhabitants} и следующими полезными ископаемыми: {_resources}");
    }
}