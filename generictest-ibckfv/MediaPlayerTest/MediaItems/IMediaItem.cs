namespace MediaPlayerTest;

public interface IMediaItem
{
    string Title { get; }
    TimeSpan Duration { get; }
    List<string> Tags { get; }
    Dictionary<string, object> Metadata { get; }
}