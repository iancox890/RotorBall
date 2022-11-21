using UnityEngine;

namespace PsychedelicGames.RotorBall
{
    /// <summary>
    /// Utility class for time.
    /// </summary>
    public static class TimeUtility
    {
        public static void Pause() => Time.timeScale = 0;
        public static void Resume() => Time.timeScale = 1;
    }
}
