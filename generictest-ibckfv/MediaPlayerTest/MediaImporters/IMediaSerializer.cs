namespace MediaPlayerTest.MediaImporters;

public interface IMediaSerializer
{
    void Serialize(Stream stream, List<IMediaItem> items);
    List<IMediaItem> Deserialize(Stream stream);
}