namespace SOLIDHomework;

public class EnderWorld : MinecraftUniverse
{
    private string[] _worldInhabitants;

    public EnderWorld(string[] inhabitants) : base("Крепость энда", 1500, 1500)
    {
        _worldInhabitants = inhabitants;
    }

    public override void GetDangerLevel()
    {
        Console.WriteLine($"Уровень опасности в {BiomName} равен 30");
    }

    public override void UniverseFeatures()
    {
        Console.WriteLine($"Ender world - мир края с нейтральными мобами, такими как {_worldInhabitants} и финальным боссом - драконом");
    }
}