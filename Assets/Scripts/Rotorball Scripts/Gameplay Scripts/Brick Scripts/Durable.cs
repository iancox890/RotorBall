using UnityEngine;

namespace PsychedelicGames.RotorBall.Gameplay
{
    /// <summary>
    /// Represents a durable brick.
    /// Takes a given amount of hits to deactivate.
    /// </summary>
    [RequireComponent(typeof(Scaler))]
    public class Durable : DestructibleBrick
    {
        [SerializeField] private int durabilityLevel;
        [SerializeField] private Transform coreTransform;

        private Scaler scaler;

        private Vector3 coreScale = Vector3.one;
        private Vector3 shrinkAmount;

        private int hitCount = 0;

        protected override void Init()
        {
            scaler = GetComponent<Scaler>();
            float shrinkage = 0.5f / (durabilityLevel - 1);
            shrinkAmount = new Vector3(shrinkage, shrinkage, shrinkage);
        }

        protected override void OnBallCollision(Transform other)
        {
            hitCount++;
            Ball sourceBall = other.GetComponent<Ball>();
            sourceBall.DurableBricksHit++;

            if (hitCount < durabilityLevel)
            {
                scoring.RewardPoints();

                sourceBall.BallScore += scoring.Score;
                sourceBall.BricksHit++;

                if (scoring.IsBonus)
                {
                    sourceBall.BonusBricksHit++;
                }

                scaler.Scale(coreTransform, coreScale, coreScale -= shrinkAmount);
            }
            else
            {
                sourceBall.DurableBricksDestroyed++;
                Destroy(sourceBall);
            }
        }
    }
}
