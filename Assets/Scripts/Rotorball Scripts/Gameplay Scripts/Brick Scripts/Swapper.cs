using UnityEngine;

namespace PsychedelicGames.RotorBall.Gameplay
{
    /// <summary>
    /// Handles brick swapping.
    /// </summary>
    public class Swapper : MonoBehaviour
    {
        [SerializeField] private BrickToSwap[] bricksToSwap;
        [SerializeField] private DestructibleBrick destructibleBrick;
        [Space]
        [SerializeField] private float initialDelay = -1;

        private IntervalTimer timer;
        private Scaler scaler;
        private int length;
        private int index;
        private bool initialSwap;

        private void Awake()
        {
            timer = GetComponent<IntervalTimer>();
            scaler = GetComponent<Scaler>();

            length = bricksToSwap.Length;
            index = -1;

            if (initialDelay != -1)
            {
                initialSwap = true;
            }

            //Ensure only the first brick is active at start.
            for (int i = 1; i < length; i++)
            {
                bricksToSwap[i].transform.gameObject.Deactivate();
            }

            SwapIn();
        }

        private void OnEnable()
        {
            destructibleBrick.OnBrickDestroyed += Disable;
            timer.OnIntervalReached += SwapOut;
        }

        private void OnDisable()
        {
            destructibleBrick.OnBrickDestroyed -= Disable;
            timer.OnIntervalReached -= SwapOut;
            gameObject.SetActive(false);
        }

        private void Disable() => enabled = false;

        private void SwapOut() => scaler.Scale(bricksToSwap[index].transform, Vector3.one, Vector3.zero, SwapIn);

        private void SwapIn()
        {
            if (index != -1)
            {
                bricksToSwap[index].transform.gameObject.Deactivate();
            }

            index = index < length - 1 ? index + 1 : 0;

            BrickToSwap brick = bricksToSwap[index];

            scaler.Scale(brick.transform, Vector3.zero, Vector3.one);

            brick.transform.gameObject.Activate();

            if (initialSwap)
            {
                timer.Interval = initialDelay;
                initialSwap = false;
            }
            else
            {
                timer.Interval = brick.interval;
            }
        }
    }

    [System.Serializable]
    internal struct BrickToSwap
    {
        [SerializeField] internal Transform transform;
        [SerializeField] internal float interval;
    }
}
