using UnityEngine;
// using System.Collections.Generic;
using System.Linq;

namespace PsychedelicGames.RotorBall.Files
{
    using Objectives.Achievments;
    /// <summary>
    /// Holds the statistics for the game.
    /// </summary>
    [System.Serializable]
    public struct StatisticsFile : IFileData
    {
        // public static void SetValue(string key, int value) {
        //     StatisticsFile temp = StatisticsFile.File;
        //     temp.GetType().GetProperty(key).SetValue(temp,value);
        //     File = temp;
        // }
        // public static void SetValue(string key, float value) {
        //     StatisticsFile temp = StatisticsFile.File;
        //     temp.GetType().GetProperty(key).SetValue(temp,value);
        //     File = temp;
        // }
        // public static void SetValue(string key, double value) {
        //     StatisticsFile temp = StatisticsFile.File;
        //     temp.GetType().GetProperty(key).SetValue(temp,value);
        //     File = temp;
        // }
        // public static void SetValue(string key, object value) {
        //     StatisticsFile temp = StatisticsFile.File;
        //     System.Reflection.PropertyInfo info = temp.GetType().GetProperty(key);
        //     info.SetValue(temp,value);
        //     File = temp;
        // }

        public const string FileName = "Statistics";

        public static StatisticsFile File
        {
            get
            {
                return FileUtility.GetFile<StatisticsFile>(FileName);
            }
            set
            {
                AchievementManager.CheckAchievements(value,false);
                FileUtility.OverwriteFile<StatisticsFile>(value,FileName);
            }
        }

        public float timePlayedTotal { get; set; }
        public float timePlayedMenu { get; set; }
        public float timePlayedGame { get; set; }

        private int totalScore;
        public int TotalScore { get => totalScore;
            set { totalScore = value; AchievementManager.SubmitStatistic("totalScore",totalScore); } }

        private int levelsCompleted;
        private int levelsFailed;
        private int levelsRestarted;
        private int levelsPlayed;
        public int LevelsCompleted { get => levelsCompleted; set => levelsCompleted = value; }
        public int LevelsFailed { get => levelsFailed; set => levelsFailed = value; }
        public int LevelsRestarted { get => levelsRestarted; set => levelsRestarted = value; }
        public int LevelsPlayed { get => levelsPlayed; set => levelsPlayed = value; }

        private int ballsLaunched;
        private int ballsSpawned;
        private int ballsLost;
        public int BallsLaunched { get => ballsLaunched; set => ballsLaunched = value; }
        public int BallsSpawned { get => ballsSpawned; set => ballsSpawned = value; }
        public int BallsLost { get => ballsLost; set => ballsLost = value; }
        public int BallsLeftover { get; set; }

        private int totalBricksHit;
        private int totalBricksDestroyed;
        public int TotalBricksHit { get => totalBricksHit; set => totalBricksHit = value; }
        public int TotalBricksDestroyed { get => totalBricksDestroyed; set => totalBricksDestroyed = value; }
        public int StandardBricksDestroyed { get; set; }
        public int DurableBricksHit { get; set; }
        public int DurableBricksDestroyed { get; set; }
        public int SwapperBricksDestroyed { get; set; }
        public int ShifterBricksDestroyed { get; set; }
        public int HazardBricksHit { get; set; }
        public int BonusBricksHit { get; set; }
        public int BonusBricksDestroyed { get; set; }
        public int PowerBricksHit { get; set; }

        public int giftsOpened { get; set; }

        public int TotalBoostsUsed { get; set; }
        public int ChainBoostsUsed { get; set; }
        public int EnduranceBoostsUsed { get; set; }
        public int EnlargeBoostsUsed { get; set; }
        private int _scoreBoostsUsed;
        public int ScoreBoostsUsed { get; set; }
        private int shrinkBoostsUsed;
        public int ShrinkBoostsUsed { get => shrinkBoostsUsed; set => shrinkBoostsUsed = value; }
        public int SuperchargeBoostsUsed { get; set; }

        public int RPEarned { get; set; }
        private int _RPSpent;
        public int RPSpent { get => _RPSpent; set => _RPSpent = value; }

        public uint TotalRotations { get; set; }
    }
}
