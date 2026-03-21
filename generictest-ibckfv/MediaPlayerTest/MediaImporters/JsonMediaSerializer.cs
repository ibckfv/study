using System.Text.Json;

namespace MediaPlayerTest.MediaImporters;

public class JsonMediaSerializer : IMediaSerializer
{
    public void Serialize(Stream output, List<IMediaItem> items)
    {
        JsonSerializer.Serialize(output, items);
    }

    public List<IMediaItem>? Deserialize(Stream input)
    {
        return JsonSerializer.Deserialize<List<IMediaItem>>(input);
    }
}