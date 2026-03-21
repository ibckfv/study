namespace MediaPlayerTest.MediaImporters;

public interface IMediaSerializerFactory
{
    IMediaSerializer GetSerializer(string path);
}