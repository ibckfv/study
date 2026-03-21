using System.Diagnostics;
using MediaPlayerTest.MediaRepository;

namespace MediaPlayerTest.MediaImporters;

public class ZipMediaImporter
{
    private IRepository<IMediaItem> _repository;
    private IMediaSerializer _serializer;
    
    public List<IMediaItem> Import(string zipPath)
    {
        throw new NotImplementedException();
    }
}