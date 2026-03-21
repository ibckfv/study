namespace MediaPlayerTest;

public class EBook : IMediaItem
{
    public string Title { get; }
    public TimeSpan Duration { get; }
    public List<string> Tags { get; }
    public Dictionary<string, object> Metadata { get; }
    public int PagesCount { get; }

    public EBook(string title, TimeSpan duration, List<string> tags, Dictionary<string, object> metadata, int pagesCount)
    {
        Title = title;
        Duration = duration;
        Tags = tags;
        Metadata = metadata;
        PagesCount = pagesCount;
    }
}