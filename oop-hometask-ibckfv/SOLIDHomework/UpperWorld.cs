namespace SOLIDHomework;

public class UpperWorld : MinecraftUniverse
{
    private string[] _worldInhabitants;
    private UsefulResources _resources;

    public UpperWorld(string[] inhabitants, UsefulResources resources) : base("Лесной биом", 30000, 30000)
    {
        _worldInhabitants = inhabitants;
        _resources = resources;
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