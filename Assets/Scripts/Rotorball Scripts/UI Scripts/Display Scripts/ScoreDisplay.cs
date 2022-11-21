using UnityEngine;
using UnityEngine.UI;
using PsychedelicGames.RotorBall.LevelManagement;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Displays the score/multiplier for the level.
    /// </summary>
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] private Text scoreText;
        [SerializeField] private Text multiplerText;

        private LevelManager manager;

        private void Awake() => manager = FindObjectOfType<LevelManager>();

        private void OnEnable()
        {
            manager.OnScoreUpdated += UpdateScore;
            manager.OnMultiplierUpdated += UpdateMultiplier;
        }

        private void OnDisable()
        {
            manager.OnScoreUpdated -= UpdateScore;
            manager.OnMultiplierUpdated -= UpdateMultiplier;
        }

        private void UpdateScore(int score) => scoreText.text = score.FormatValue();
        private void UpdateMultiplier(float multiplier) => multiplerText.text = multiplier.ToString("0.00") + "x";
    }
}