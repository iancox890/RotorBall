using UnityEngine;

namespace PsychedelicGames.RotorBall.Gameplay
{
    /// <summary>
    /// Manages the bonus brick(s) for a given level.
    /// </summary>
    public class BonusManager : MonoBehaviour
    {
        [SerializeField] private Material bonusMat;
        [SerializeField] private int amount;
        [SerializeField] private int deactivationMultiplier;

        private Scoring[] activeBricks;

        private Vector2 positiveInfinity = Vector2.positiveInfinity;
        private Vector2 negativeInfinity = Vector2.negativeInfinity;

        private int mask;
        private int totalBricks;
        private int deactivationAmount;

        public void Add()
        {
            if (DestructibleBrick.Bricks <= deactivationAmount)
            {
                return;
            }
            Scoring.Scorings[Random.Range(0, Scoring.Scorings.Count)].AddAsBonus(bonusMat);
        }

        public void AddAll()
        {
            int previousIndex = 0;
            int newIndex;
            int activeCount = Scoring.Scorings.Count;

            if (activeCount > 1)
            {
                for (int i = 0; i < amount; i++)
                {
                    do
                    {
                        newIndex = Random.Range(0, activeCount);
                    }
                    while (newIndex == previousIndex);

                    previousIndex = newIndex;
                    Scoring.Scorings[newIndex].AddAsBonus(bonusMat);
                }
            }
        }

        private void Start()
        {
            deactivationAmount = amount * deactivationMultiplier;
            mask = 1 << 9;
            AddAll();
        }
    }
}
