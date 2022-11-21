using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.UI {
    using LevelManagement;
    public class LeaderboardScore : MonoBehaviour
    {
        [SerializeField] private Text _playerName;
        [SerializeField] private Text _playerScore;
        // Start is called before the first frame update

        public void SetScore(Leaderboard.Score score)
        {
            SetScore(score.name, score.score);
        }
        public void SetScore(string name, int score)
        {
            _playerName.text = name;
            _playerScore.text = score.ToString();
        }
    }
}