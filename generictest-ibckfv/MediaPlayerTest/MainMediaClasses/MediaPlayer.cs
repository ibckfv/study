using MediaPlayerTest.MediaRepository;

namespace MediaPlayerTest;

public class MediaPlayer
{
    private Repository<IMediaItem> _repository;
    private IPlaybackState _state = new StoppedState();
    public event Action<string>? OnStateChanged;

    public void ChangeState(IPlaybackState state)
    {
        _state = state;
        OnStateChanged?.Invoke(_state.GetType().Name);
    }

    public void Play()
    {
        _state.Play(this);
    }
    public void Pause()
    {
        _state.Pause(this);
    }

    public void Stop()
    {
        _state.Stop(this);
    }
}