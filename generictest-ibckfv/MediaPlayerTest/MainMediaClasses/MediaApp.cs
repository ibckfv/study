using MediaPlayerTest.MediaImporters;
using MediaPlayerTest.MediaRepository;

namespace MediaPlayerTest;

public class MediaApp
{
    private List<IRepository<IMediaItem>> _allRepositories = new List<IRepository<IMediaItem>>();
    private List<IMediaItem> _playedMedias = new List<IMediaItem>();
    private ZipMediaImporter _mediaImporter;
    private MediaPlayer _mediaPlayer;

    public MediaApp(MediaPlayer player)
    {
        _mediaPlayer = player;
    }
    
    public void PlayItem(string path)
    {
    }

    public void ImportFromZip(string zipPath)
    {
        _mediaImporter.Import(zipPath);
    }

    public void AddRepository(IRepository<IMediaItem> repository)
    {
        _allRepositories.Add(repository);
    }

    public List<IRepository<IMediaItem>> GetAllRepositories()
    {
        return _allRepositories;
    }

    public List<IMediaItem> ShowHistory()
    {
        return _playedMedias;
    }
}