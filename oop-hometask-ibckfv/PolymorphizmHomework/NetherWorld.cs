namespace PolymorphizmHomework;

public class NetherWorld : MinecraftUniverse
{
    public string[] WorldInhabitants { get; set; }
    public string[] Resources { get; set; }

    public NetherWorld(string[] inhabitants, string[] resources) : base("Лавовые озёра", 3000, 3000)
    {
        WorldInhabitants = inhabitants;
        Resources = resources;
    }

    public override void GetDangerLevel()
    {
        Console.WriteLine($"Уровень опасности в {BiomName} равен 100");
    }

    public override void UniverseFeatures()
    {
        Console.WriteLine("Nether world - мир ада с исключительно враждебными мобами, " +
                          $"такими как {WorldInhabitants} и следующими полезными ископаемыми: {Resources}");
    }
}