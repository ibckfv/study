namespace HierarchyHomework;

public class UpperWorld : MinecraftUniverse
{
    public string[] WorldInhabitants {  get; set; }
    public string[] Resources { get; set; }

    public UpperWorld(string[] inhabitants, string[] resources) : base("Лесной биом", 30000, 30000)
    {
        WorldInhabitants = inhabitants;
        Resources = resources;
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