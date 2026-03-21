namespace StructsHomework;

public class UpperWorld : MinecraftUniverse
{
    private string[] _worldInhabitants;
    public UsefulResources Resources { get; }
    public ClimaticConditions Climate { get; }

    public UpperWorld(string[] inhabitants, UsefulResources resources, ClimaticConditions climate) : base("Лесной биом", 30000, 30000)
    {
        _worldInhabitants = inhabitants;
        Resources = resources;
        Climate = climate;
    }

    public override void GetDangerLevel()
    {
        Console.WriteLine($"Ураень опасности в {BiomName} равен 10");
    }

    public override void UniverseFeatures()
    {
        Console.WriteLine("Upper world - мир с дневным и ночным циклом, враждебными и дружелюбными мобами, " +
                          $"такими как {_worldInhabitants} и следующими полезными ископаемыми: {Resources}");
    }
}