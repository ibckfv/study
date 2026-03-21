namespace HierarchyHomework;

public class Program
{
    static void Main()
    {
        var upperWorld = new UpperWorld(["zombie", "spider", "creeper", "chicken", "cow", "rabbit"],
            ["coal", "redstone", "iron", "diamond", "emerald"]);
        upperWorld.UniverseFeatures();
        upperWorld.GetDangerLevel();
        var netherWorld = new NetherWorld(["piglin", "blaze", "ghast", "skeleton"],
            ["gold", "quartz", "netherit"]);
        netherWorld.UniverseFeatures();
        netherWorld.GetDangerLevel();
        var enderWorld = new EnderWorld(["enderman", "shalker"]);
        enderWorld.UniverseFeatures();
        enderWorld.GetDangerLevel();
        var bookshelf = new Bookshelf(10, 15);
        bookshelf.CalculateArea();
    }
}    

