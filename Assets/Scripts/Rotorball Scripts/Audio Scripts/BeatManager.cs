using UnityEngine;

namespace PsychedelicGames.RotorBall.Audio
{
    /// <summary>
    /// Manager for beat detection/visuals.
    /// </summary>
    public class BeatManager : MonoBehaviour
    {
        [SerializeField] private BeatDetector[] detectors;

        private int length;

        private void Start() => length = detectors.Length;

        private void Update()
        {
            for (int i = 0; i < length; i++) { detectors[i].Detect(); }
        }
    }
}
