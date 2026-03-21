namespace StructsHomework;

public struct ClimaticConditions
{
    public string Temperature { get; set; }
    public string Precipitation { get; set; }
    public string Season { get; set; }

    public ClimaticConditions(string temperature, string precipitation, string season)
    {
        Temperature = temperature;
        Precipitation = precipitation;
        Season = season;
    }
    
    public override string ToString()
    {
        return $"Климатические условия: {Season}, {Precipitation}, {Temperature}.";
    }
}