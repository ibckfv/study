namespace PolymorphizmHomework;

public class Program
{
    static void Main()
    {
        var upperWorld = new UpperWorld(["zombie", "spider", "creeper", "chicken", "cow", "rabbit"],
           ["coal", "redstone", "iron", "diamond", "emerald"]);
        var netherWorld = new NetherWorld(["piglin", "blaze", "ghast", "skeleton"],
            ["gold", "quartz", "netherit"]);

        //Пример статического ad-hoc полиморфизма
        upperWorld.GetResourceUsage(1);
        upperWorld.GetResourceUsage("iron");

        //Пример динамического ad-hoc полиморфизма
        MinecraftUniverse minecraftUniverse = new NetherWorld([], []);
        minecraftUniverse.GetDangerLevel();
        minecraftUniverse = new EnderWorld([]);
        minecraftUniverse.GetDangerLevel();

        //Пример полиморфизма подтипов
        upperWorld.CalculateArea();
        netherWorld.CalculateArea();

        //Пример метода расширения
        upperWorld.ChangeBiom("Пустынный биом");
    }
}    

