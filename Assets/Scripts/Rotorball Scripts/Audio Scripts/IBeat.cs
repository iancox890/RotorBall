namespace PsychedelicGames.RotorBall.Audio
{
    public interface IBeat
    {
        void OnBeat(float deltaTime, float intensity);
        void OffBeat(float deltaTime);
    }
}