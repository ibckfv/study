namespace MediaPlayerTest.MediaRepository;

public interface IRepository<TMediaItem>
{
    void Add(TMediaItem item);
    IEnumerable<TMediaItem> GetAll();
    IEnumerable<IMediaItem> Find(Predicate<TMediaItem> function);
}