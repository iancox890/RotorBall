using UnityEngine;

namespace PsychedelicGames.RotorBall.Gameplay
{
    /// <summary>
    /// Works on a set interval.
    /// </summary>
    public class IntervalTimer : MonoBehaviour
    {
        private float interval;
        private float compareTime;

        public float Interval
        {
            get => interval;
            set
            {
                interval = value;
                compareTime = Time.timeSinceLevelLoad + interval;
            }
        }

        public event System.Action OnIntervalReached;

        private void Start() => compareTime = interval;

        private void Update()
        {
            float time = Time.timeSinceLevelLoad;
            if (time > compareTime)
            {
                compareTime = time + interval;
                OnIntervalReached?.Invoke();
            }
        }
    }
}
