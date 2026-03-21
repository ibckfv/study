namespace MediaPlayerTest;

public class PausedState : IPlaybackState
{
    public void Play(MediaPlayer player)
    {
        Console.WriteLine("Playing");
        player.ChangeState(new PlayingState());
    }

    public void Pause(MediaPlayer player)
    {
        Console.WriteLine("Already paused");
    }

    public void Stop(MediaPlayer player)
    {
        Console.WriteLine("Stopped");
        player.ChangeState(new StoppedState());
    }
}