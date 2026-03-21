namespace StructsHomework;

public class NetherWorld : MinecraftUniverse
{
    private string[] _worldInhabitants;
    public UsefulResources Resources { get; }
    public ClimaticConditions Climate { get; }

    public NetherWorld(string[] inhabitants, UsefulResources resources, ClimaticConditions climate) : base("Лавовые озёра", 3000, 3000)
    {
        _worldInhabitants = inhabitants;
        Resources = resources;
        Climate = climate;
    }

    public override void GetDangerLevel()
    {
        Console.WriteLine($"Уровень опасности в {BiomName} равен 100");
    }

    public override void UniverseFeatures()
    {
        Console.WriteLine("Nether world - мир ада с исключительно враждебными мобами, " +
                          $"такими как {_worldInhabitants} и следующими полезными ископаемыми: {Resources}");
    }
}