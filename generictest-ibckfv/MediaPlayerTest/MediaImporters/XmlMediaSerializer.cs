using System.Xml.Serialization;

namespace MediaPlayerTest.MediaImporters;

public class XmlMediaSerializer : IMediaSerializer
{
    public void Serialize(Stream output, List<IMediaItem> items)
    {
        new XmlSerializer(typeof(List<IMediaItem>)).Serialize(output, items);
    }

    public List<IMediaItem>? Deserialize(Stream input)
    {
        return new XmlSerializer(typeof(List<IMediaItem>))
            .Deserialize(input) as List<IMediaItem>;
    }
}