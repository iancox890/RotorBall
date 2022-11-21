using UnityEngine;
using PsychedelicGames.RotorBall.Objectives;
using PsychedelicGames.RotorBall.Files;

namespace PsychedelicGames.RotorBall.LevelManagement
{
    using Objectives.Achievments;
    /// <summary>
    /// Data container for a given level.
    /// </summary>
    [CreateAssetMenu(fileName = "LevelData.asset", menuName = "Psychedelic Games/Levels/Level Data")]
    public class LevelData : ScriptableObject
    {
        public const int ObjectiveCount = 3;

        [SerializeField] private StringVariable description;
        [Space]
        [SerializeField] private GameScene scene;
        [Space]
        [SerializeField] private LevelModifiers modifiers;
        [Space]
        [SerializeField] private float targetTime;
        [SerializeField] private Objective[] objectives = new Objective[ObjectiveCount];

        private string number;
        private LevelFile file;

        public static LevelData Current { get; set; }
        public LevelFile File { get => FileUtility.GetFile<LevelFile>(scene.SceneName); set => file = value; }

        public bool IsLocked { get; set; }

        public string Number
        {
            get
            {
                if (string.IsNullOrEmpty(number))
                {
                    string name = scene.SceneName;
                    number = name.Substring(name.LastIndexOf('_') + 1);
                }
                return number;
            }
        }

        public string Description { get => description.Value; }
        public GameScene Scene { get => scene; }
        public LevelModifiers Modifiers { get => modifiers; }
        public float TargetTime { get => targetTime; }
        public Objective[] Objectives { get => objectives; }

        public int FailedAttempts = 0;

        public int UpdateData(LevelReport report)
        {
            if (file == null) { file = File; }
            file.IsCompleted = true;

            if (FailedAttempts >= 3) {
                AchievementManager.Achieve("Practice Makes Perfect");
            }

            bool[] objectivesCompleted = file.ObjectivesCompleted;
            int completed = 0;

            float reportTime = report.FinalTime;
            int reportScore = report.TotalScore;

            for (int i = 0; i < ObjectiveCount; i++)
            {
                if (!objectivesCompleted[i])
                {
                    bool isCompleted = objectives[i].UpdateStatus(report);
                    objectivesCompleted[i] = isCompleted;

                    if (isCompleted) { completed++; }
                }
            }
            file.ObjectivesCompleted = objectivesCompleted;

            if (reportTime < file.RecordTime)
            {
                file.RecordTime = reportTime;
            }
            if (reportScore > file.RecordScore)
            {
                file.RecordScore = reportScore;
            }

            FileUtility.OverwriteFile(file, scene.SceneName);

            return completed;
        }

        public void ResetData()
        {
            file = new LevelFile();
            FileUtility.OverwriteFile(file, scene.SceneName);
        }
    }
}
