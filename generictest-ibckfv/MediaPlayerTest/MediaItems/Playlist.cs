namespace MediaPlayerTest;

public class Playlist
{
    public List<IMediaItem> Items { get; }
    public string Name { get; }

    public Playlist(string name, List<IMediaItem> items)
    {
        Name = name;
        Items = items;
    }

    public void Add(IMediaItem item)
    {
        Items.Add(item);
    }
}