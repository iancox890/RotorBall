using UnityEngine;
using UnityEngine.UI;
using PsychedelicGames.RotorBall.LevelManagement;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Display the rewards for a given level.
    /// </summary>
    public class RewardsText : MonoBehaviour
    {
        [SerializeField] private Reward reward;
        [SerializeField] private GameObject header;
        private LevelManager manager;

        private enum Reward { RP, RPFromScore, XP, Score, TotalScore, TimeBonus, BallBonus, ObjectiveBonus, FirstTimeBonus }

        private void Start()
        {
            manager = FindObjectOfType<LevelManager>();
            Text rewardsText = GetComponent<Text>();
            float value = 0;

            switch (reward)
            {
                case (Reward.RP):
                    value = manager.RotorPointsEarned;
                    break;
                case (Reward.RPFromScore):
                    value = manager.RpFromScore;
                    break;
                case (Reward.XP):
                    value = manager.ExperiencePointsEarned;
                    break;
                case (Reward.Score):
                    value = manager.Score;
                    break;
                case (Reward.TotalScore):
                    value = manager.TotalScore;
                    break;
                case (Reward.TimeBonus):
                    value = manager.ScoreFromTime;
                    break;
                case (Reward.BallBonus):
                    value = manager.ScoreFromBalls;
                    break;
                case (Reward.ObjectiveBonus):
                    value = manager.RpFromObjectives;
                    break;
                case (Reward.FirstTimeBonus):
                    value = manager.FirstTimeBonus;
                    break;
            }

            if (value < 1 && header != null) { header.Deactivate(); }
            else { rewardsText.text = value.FormatValue(); }
        }
    }
}
