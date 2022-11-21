using UnityEngine;
using System;

namespace PsychedelicGames.RotorBall.Gameplay
{

    using LevelManagement;

    /// <summary>
    /// Base class for all destructible bricks.
    /// </summary>
    public class DestructibleBrick : MonoBehaviour
    {
        private static int bricks = 0;
        public static int Bricks { get => bricks; }

        private Sequence sequence;

        private bool isDestroyed;
        private bool inSequence;

        protected Scoring scoring;

        public static event Action OnAllBricksDestroyed;
        public event Action OnBrickDestroyed;

        public Ball SourceBall { get; set; }

        public void Destroy(Ball sourceBall)
        {
            if (!isDestroyed && enabled)
            {
                isDestroyed = true;

                bricks--;
                SourceBall = sourceBall;

                scoring.RewardPoints();

                sourceBall.BallScore += scoring.Score;
                sourceBall.BricksHit++;
                sourceBall.BricksDestroyed++;

                if (GetComponent<Shifter>() != null)
                {
                    sourceBall.ShifterBricksDestroyed++;
                }
                if (transform.parent.parent.gameObject.GetComponent<Swapper>() != null)
                {
                    sourceBall.SwapperBricksDestroyed++;
                }
                if (scoring.IsBonus)
                {
                    sourceBall.BonusBricksHit++;
                    sourceBall.BonusBricksDestroyed++;
                }

                if (inSequence) { sequence.UpdateSequence(); }
                if (bricks < 1) { FindObjectOfType<LevelManager>().UpdateReport(sourceBall); OnAllBricksDestroyed?.Invoke(); }

                OnBrickDestroyed?.Invoke();
                gameObject.Deactivate();
            }
        }

        public void AddToSequence(Sequence sequence)
        {
            inSequence = true;
            this.sequence = sequence;
        }

        protected virtual void Init() { }
        protected virtual void OnBallCollision(Transform other) { }

        private void Awake()
        {
            scoring = GetComponent<Scoring>();
            bricks++;

            Init();
        }

        private void OnDestroy() => bricks = 0;

        private void OnCollisionEnter2D(Collision2D other)
        {
            Transform otherTrans = other.transform;
            if (otherTrans.CompareTag("Player")) { OnBallCollision(otherTrans); }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Transform otherTrans = other.transform;
            if (otherTrans.CompareTag("Player")) { OnBallCollision(otherTrans); }
        }
    }
}
