using UnityEngine;
using PsychedelicGames.RotorBall.LevelManagement;
using System.Collections.Generic;

namespace PsychedelicGames.RotorBall.Gameplay
{
    /// <summary>
    /// Handles how the player is rewarded for hitting/destroying a brick.
    /// </summary>
    public class Scoring : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer mainRenderer;
        [SerializeField] private SpriteRenderer hollowRenderer;
        [Space]
        [SerializeField] private ScoringValue pointValue;
        [SerializeField] private ScoringValue pointValueBonus;

        private LevelManager levelManager;
        private BonusManager bonusManager;
        private ScoringValue current;
        private DestructibleBrick destructible;
        private static List<Scoring> scorings = new List<Scoring>();
        public static List<Scoring> Scorings { get => scorings; }

        private bool isBonus;
        public bool IsBonus { get => isBonus; }

        private int score;
        public int Score { get => score; set => score = value; }

        private Material mainMat;
        private Material hollowMat;

        public void RewardPoints()
        {
            if (levelManager)
            {
                score = Mathf.RoundToInt(current.scoreValue * levelManager.Multiplier);
                levelManager.Score += score;
                levelManager.Multiplier += current.multiplierValue;
            }
        }

        public void AddAsBonus(Material bonusMat)
        {
            isBonus = true;
            current = pointValueBonus;

            mainRenderer.material = bonusMat;
            hollowRenderer.material = bonusMat;
        }

        private void Awake()
        {
            levelManager = FindObjectOfType<LevelManager>();
            bonusManager = FindObjectOfType<BonusManager>();
            destructible = GetComponent<DestructibleBrick>();

            mainMat = mainRenderer.material;
            hollowMat = hollowRenderer.material;

            current = pointValue;
        }

        private void OnEnable()
        {
            destructible.OnBrickDestroyed += OnDestroyed;
            scorings.Add(this);
        }

        private void OnDisable()
        {
            destructible.OnBrickDestroyed -= OnDestroyed;
            scorings.Remove(this);
        }

        private void OnDestroyed()
        {
            if (isBonus)
            {
                scorings.Remove(this);
                bonusManager.Add();
            }
        }
    }

    [System.Serializable]
    internal struct ScoringValue
    {
        [SerializeField] internal int scoreValue;
        [SerializeField] internal float multiplierValue;
    }
}
