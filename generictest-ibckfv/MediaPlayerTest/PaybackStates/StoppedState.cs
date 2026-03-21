namespace MediaPlayerTest;

public class StoppedState : IPlaybackState
{
    public void Play(MediaPlayer player)
    {
        Console.WriteLine("Playing");
        player.ChangeState(new PlayingState());
    }

    public void Pause(MediaPlayer player)
    {
        Console.WriteLine("Paused");
        player.ChangeState(new PausedState());
    }

    public void Stop(MediaPlayer player)
    {
        Console.WriteLine("Already stopped");
    }
}