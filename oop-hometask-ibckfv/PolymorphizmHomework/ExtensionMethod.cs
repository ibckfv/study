namespace PolymorphizmHomework;

public static class ExtensionMethod
{
    public static void ChangeBiom(this MinecraftUniverse universe, string biomName)
    {
        universe.BiomName = biomName;
    }
}
