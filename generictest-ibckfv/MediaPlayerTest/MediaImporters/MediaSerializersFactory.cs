namespace MediaPlayerTest.MediaImporters;

public class MediaSerializersFactory : IMediaSerializerFactory
{
    public IMediaSerializer GetSerializer(string path)
    {
        switch (Path.GetExtension(path).ToLower())
        {
            case ".json":
                return new JsonMediaSerializer();
            case ".xml":
                return new XmlMediaSerializer();
            default:
                throw new Exception("type not supported");
                break;
        }
    }
}