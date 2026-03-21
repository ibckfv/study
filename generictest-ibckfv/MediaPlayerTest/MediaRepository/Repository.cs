namespace MediaPlayerTest.MediaRepository;

public class Repository<TMediaItem> : IRepository<TMediaItem>
{
    private List<Playlist> _playlists;
    
    public void Add(TMediaItem item)
    {
    }

    public IEnumerable<TMediaItem> GetAll()
    {
        return (IEnumerable<TMediaItem>)_playlists;
    }

    public IEnumerable<IMediaItem> Find(Predicate<TMediaItem> function)
    {
        throw new NotImplementedException();
    }
}