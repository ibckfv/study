using System.Text.Json;
using YamlDotNet.Serialization;

namespace StreamsLinqHomework;

public class ParkingSpacesAdapter
{
    private List<ParkingSpace> _spaces;

    public ParkingSpacesAdapter(List<ParkingSpace> parkingSpaces)
    {
        _spaces = parkingSpaces;
    }

    public void SaveToJsonFile(string pathToJsonFile)
    {
        string json = JsonSerializer.Serialize(_spaces);
        File.WriteAllText(pathToJsonFile, json);
    }

    //Не нашёл библиотеку YamlStream, использовал YamlDotNet
    
    public List<ParkingSpace> LoadFromYamlFile(string pathToYamlFile)
    {
        string yaml = File.ReadAllText(pathToYamlFile);
        var deserializer = new DeserializerBuilder().Build();
        return deserializer.Deserialize<List<ParkingSpace>>(yaml);
    }
}