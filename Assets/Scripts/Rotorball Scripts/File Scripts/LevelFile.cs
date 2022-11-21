using UnityEngine;

namespace PsychedelicGames.RotorBall.Files
{
    /// <summary>
    /// Stores persistant data related to a given level.
    /// This includes things such as objectives completed, records, etc.
    /// </summary>
    [System.Serializable]
    public class LevelFile : IFileData
    {
        private bool isCompleted;
        private bool[] objectivesCompleted;
        private float recordTime;
        private int recordScore;

        public bool IsCompleted { get => isCompleted; set => isCompleted = value; }
        public bool[] ObjectivesCompleted { get => objectivesCompleted; set => objectivesCompleted = value; }
        public float RecordTime { get => recordTime; set => recordTime = value; }
        public int RecordScore { get => recordScore; set => recordScore = value; }

        public LevelFile()
        {
            isCompleted = false;
            objectivesCompleted = new bool[] { false, false, false };
            recordTime = float.MaxValue;
            recordScore = 0;
        }
    }
}
