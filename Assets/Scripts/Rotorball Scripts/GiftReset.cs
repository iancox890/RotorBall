using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PsychedelicGames.RotorBall
{
    using Files;

    /// <summary>
    /// Stores the amount of time it takes to unlock gift wheel spins
    /// </summary>
    [CreateAssetMenu(fileName = "GiftReset", menuName = "Psychedelic Games/Gift Reset")]
    public class GiftReset : ScriptableObject
    {
        [SerializeField] private int[] giftResetTimesInMinutes;
        [SerializeField] private int giftSpeedupTimeInMinutes;
        public int GiftSpeedupTimeInMinutes { get => giftSpeedupTimeInMinutes; }

        public static System.TimeSpan TimeElapsed()
        {
            PlayerFile file = PlayerFile.GetFile();
            System.TimeSpan dif = System.DateTime.Now - file.giftLastClaimed;
            return dif;
        }

        public System.TimeSpan TimeRemaining()
        {
            StatisticsFile file = StatisticsFile.File;
            int giftTime = giftResetTimesInMinutes[Mathf.Min(file.giftsOpened, giftResetTimesInMinutes.Length - 1)];

            return new System.TimeSpan(0, giftTime, 0) - TimeElapsed();
        }

        public string TimeRemainingFormatted()
        {
            System.TimeSpan remaining = TimeRemaining();
            return FormatTime(remaining);
        }

        public static string FormatTime(System.TimeSpan remaining)
        {
            return string.Format("{0:D2}:{1:D2}:{2:D2}", remaining.Hours, remaining.Minutes, remaining.Seconds);
        }
        public static string FormatTime(long ticks)
        {
            return FormatTime(new System.TimeSpan(ticks));
        }

        public bool IsUnlocked()
        {
            return TimeRemaining().TotalSeconds <= 0;
        }

        public void Lock()
        {
            PlayerFile file = PlayerFile.GetFile();
            file.giftLastClaimed = System.DateTime.Now;
            PlayerFile.file = file;
            PlayerFile.SaveFile();

            StatisticsFile statFile = StatisticsFile.File;
            statFile.giftsOpened++;
            StatisticsFile.File = statFile;
        }

        public void Unlock()
        {
            StatisticsFile file = StatisticsFile.File;
            int giftTime = giftResetTimesInMinutes[Mathf.Min(file.giftsOpened, giftResetTimesInMinutes.Length - 1)];

            PlayerFile plrFile = PlayerFile.GetFile();
            plrFile.giftLastClaimed = System.DateTime.Now.AddMinutes(-giftTime);
            PlayerFile.file = plrFile;
            PlayerFile.SaveFile();
        }

        public void Speedup()
        {
            PlayerFile file = PlayerFile.GetFile();
            file.giftLastClaimed = file.giftLastClaimed.AddMinutes(-giftSpeedupTimeInMinutes);
            PlayerFile.file = file;
            PlayerFile.SaveFile();
        }
    }
}