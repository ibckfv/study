namespace HierarchyHomework;

public class EnderWorld : MinecraftUniverse
{
    public string[] WorldInhabitants { get; set; }

    public EnderWorld(string[] inhabitants) : base("Крепость энда", 1500, 1500)
    {
        WorldInhabitants = inhabitants;
    }

    public override void GetDangerLevel()
    {
        Console.WriteLine($"Уровень опасности в {BiomName} равен 30");
    }

    public override void UniverseFeatures()
    {
        Console.WriteLine($"Ender world - мир края с нейтральными мобами, такими как {WorldInhabitants} и финальным боссом - драконом");
    }
}