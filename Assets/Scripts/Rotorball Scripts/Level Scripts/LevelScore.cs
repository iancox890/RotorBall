using UnityEngine;
using System;

namespace PsychedelicGames.RotorBall.LevelManagement
{
    /// <summary>
    /// Manages the score for a given level.
    /// </summary>
    public  class LevelScore : MonoBehaviour
    {
        [SerializeField] private float multiplierMax;

        [SerializeField] private float coolDownTime;
        private float currentCoolDown;

        private int score;
        public int Score
        {
            get => score;
            set { score = value; OnScoreUpdate?.Invoke(score); }
        }

        private float multiplier = 1;
        public float Multiplier
        {
            get => multiplier;
            set
            {
                if (multiplier != multiplierMax && multiplier + value < multiplierMax)
                {
                    multiplier = value;
                }
                else
                {
                    multiplier = multiplierMax;
                }

                OnMultiplierUpdate?.Invoke(multiplier);
                currentCoolDown = 0;
            }
        }

        public event Action<int> OnScoreUpdate;
        public event Action<float> OnMultiplierUpdate;

        private void Update()
        {
            currentCoolDown += Time.deltaTime;

            if (currentCoolDown >= coolDownTime)
            {
                ResetMultiplier();
            }
        }

        private void ResetMultiplier() => Multiplier = 1;
    }
}
