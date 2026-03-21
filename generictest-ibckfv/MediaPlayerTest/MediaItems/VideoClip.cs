namespace MediaPlayerTest;

public class VideoClip : IMediaItem
{
    public string Title { get; }
    public TimeSpan Duration { get; }
    public List<string> Tags { get; }
    public Dictionary<string, object> Metadata { get; }
    public string Resolution { get; }

    public VideoClip(string title, TimeSpan duration, List<string> tags, Dictionary<string, object> metadata, string resolution)
    {
        Title = title;
        Duration = duration;
        Tags = tags;
        Metadata = metadata;
        Resolution = resolution;
    }
}