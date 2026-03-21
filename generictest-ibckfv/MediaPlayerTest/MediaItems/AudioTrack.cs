namespace MediaPlayerTest;

public class AudioTrack : IMediaItem
{
    public string Title { get; }
    public TimeSpan Duration { get; }
    public List<string> Tags { get; }
    public Dictionary<string, object> Metadata { get; }
    public int Bitrate { get; }

    public AudioTrack(string title, TimeSpan duration, List<string> tags, Dictionary<string, object> metadata, int bitrate)
    {
        Title = title;
        Duration = duration;
        Tags = tags;
        Metadata = metadata;
        Bitrate = bitrate;
    }
}