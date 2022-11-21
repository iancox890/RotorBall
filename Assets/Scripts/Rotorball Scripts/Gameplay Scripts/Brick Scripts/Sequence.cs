using UnityEngine;

namespace PsychedelicGames.RotorBall.Gameplay
{
    /// <summary>
    /// Handles a given sequence of bricks.
    /// </summary>
    [RequireComponent(typeof(Scaler))]
    public class Sequence : MonoBehaviour
    {
        [SerializeField] private Wave[] waves;

        private Wave currentWave;
        private Scaler scaler;

        private int waveIndex;
        private int waveLength;
        private int currentDestructibleCount;

        public void UpdateSequence()
        {
            //Start the next wave if all the destructible bricks are cleared
            if (--currentDestructibleCount < 1)
            {
                currentWave.SetState(scaler, false);
                if (++waveIndex < waveLength)
                {
                    UpdateWave();
                }
            }
        }

        private void Awake()
        {
            scaler = GetComponent<Scaler>();
            waveLength = waves.Length;

            for (int i = 0; i < waveLength; i++)
            {
                waves[i].Initiate(this);
            }

            UpdateWave();
        }

        private void UpdateWave()
        {
            currentWave = waves[waveIndex];
            currentWave.SetState(scaler, true);

            currentDestructibleCount = currentWave.destructibleCount;
        }
    }

    [System.Serializable]
    internal class Wave
    {
        [SerializeField] internal GameObject[] bricks;
        [Space]
        [SerializeField] internal bool keepIndestructibles;

        internal int length;
        internal int destructibleCount;

        internal void Initiate(Sequence sequence)
        {
            length = bricks.Length;

            for (int i = 0; i < length; i++)
            {
                GameObject brick = bricks[i];
                DestructibleBrick destructibleBrick = brick.GetComponentInChildren<DestructibleBrick>();

                if (destructibleBrick)
                {
                    destructibleBrick.AddToSequence(sequence);
                    destructibleCount++;
                }

                brick.Deactivate();
            }
        }

        internal void SetState(Scaler scaler, bool active)
        {
            for (int i = 0; i < length; i++)
            {
                GameObject brick = bricks[i];
                Transform brickTransform = brick.transform;

                if (active)
                {
                    brick.Activate();
                    scaler.Scale(brickTransform, Vector3.zero, brickTransform.localScale);
                }
                else
                {
                    //Keep the brick if necessary
                    if (brick.CompareTag("Destructible") || (keepIndestructibles))
                    {
                        continue;
                    }
                    scaler.Scale(brickTransform, brickTransform.localScale, Vector3.zero, () => brick.Deactivate());
                }
            }
        }
    }
}
