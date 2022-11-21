using UnityEngine;

namespace PsychedelicGames.RotorBall.Audio
{
    /// <summary>
    /// Controls when a beat is detected for this beat visual.
    /// </summary>
    public class BeatDetector : MonoBehaviour
    {
        [SerializeField] [UnityEngine.Serialization.FormerlySerializedAs("bias")] private float sensitivity;
        [SerializeField] private float timeStep;

        private new Transform transform;
        private IBeat beat;

        private float value;
        private float previousValue;
        private float timer;

        public void Detect()
        {
            value = AudioManager.BassValue;

            float deltaTime = Time.deltaTime;

            if ((timer > timeStep) && (previousValue < sensitivity && value > sensitivity))
            {
                beat.OnBeat(deltaTime, value);
                timer = 0;
            }
            else
            {
                beat.OffBeat(deltaTime);
            }

            timer += deltaTime;
            previousValue = value;
        }

        private void Start()
        {
            beat = GetComponent<IBeat>();
            if (beat == null) { Debug.LogWarning("No IBeat implementation found! Visualisation will not work."); }
        }
    }
}
