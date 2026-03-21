namespace MediaPlayerTest;

public interface IPlaybackState
{
    void Play(MediaPlayer player);
    void Pause(MediaPlayer player);
    void Stop(MediaPlayer player);
}    