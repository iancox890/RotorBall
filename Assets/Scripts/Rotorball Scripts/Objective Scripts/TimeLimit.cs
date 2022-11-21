using UnityEngine;
using PsychedelicGames.RotorBall.LevelManagement;

namespace PsychedelicGames.RotorBall.Objectives
{
    /// <summary>
    /// TimeLimit requires the player to finish a level under a given time.
    /// </summary>
    [CreateAssetMenu(fileName = "Objective(Number)(TimeLimit).asset", menuName = "Psychedelic Games/Objectives/Time Limit")]
    public class TimeLimit : Objective
    {
        [SerializeField] private float time;
        public float Time { get=>time; }

        public override string Description { get => description.Value.Replace("<time>", time.ToFormattedTime()); }

        public override bool UpdateStatus(LevelReport report)
        {
            if (!report.Retried && report.FinalTime < time) { return true; }
            return false;
        }
    }
}
