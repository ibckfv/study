namespace MediaPlayerTest;

public class PlayingState : IPlaybackState
{
    public void Play(MediaPlayer player)
    {
        Console.WriteLine("Already playing");
    }

    public void Pause(MediaPlayer player)
    {
        Console.WriteLine("Paused");
        player.ChangeState(new PausedState());
    }

    public void Stop(MediaPlayer player)
    {
        Console.WriteLine("Stopped");
        player.ChangeState(new StoppedState());
    }
}