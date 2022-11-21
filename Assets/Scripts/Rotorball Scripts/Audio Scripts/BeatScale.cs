using UnityEngine;
using System.Collections;

namespace PsychedelicGames.RotorBall.Audio
{
    /// <summary>
    /// Adjusts an objects scale on beat.
    /// </summary>
    public class BeatScale : MonoBehaviour, IBeat
    {
        [SerializeField] private Vector3 restScale = Vector3.one;
        [SerializeField] private Vector3 beatScale = Vector3.one;
        [Space]
        [SerializeField] private float restSpeed = 5;
        [SerializeField] private float beatSpeed = 8;
        [Space]
        [SerializeField] private float intensityModifier = 8;
        [Space]
        [SerializeField] private bool lockX;
        [SerializeField] private bool lockY;

        private new Transform transform;

        private IEnumerator scaleToBeat;

        private float x;
        private float y;

        public void OnBeat(float deltaTime, float intensity)
        {
            if (scaleToBeat != null) { StopCoroutine(scaleToBeat); }
            scaleToBeat = ScaleToBeat(intensity * intensityModifier);
            StartCoroutine(scaleToBeat);
        }

        public void OffBeat(float deltaTime)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, restScale, deltaTime * restSpeed);
            UpdateLock();
        }

        private void Start()
        {
            transform = GetComponent<Transform>();
            transform.localScale = restScale;

            if (lockX || lockY)
            {
                Vector3 pos = transform.localPosition;

                x = pos.x;
                y = pos.y;
            }
        }

        private void UpdateLock()
        {
            if (lockX || lockY)
            {
                Vector3 pos = transform.localPosition;
                Vector3 scale = transform.localScale;

                if (lockX)
                {
                    pos.x = x + (scale.x * 0.5f);
                }
                if (lockY)
                {
                    pos.y = y + (scale.y * 0.5f);
                }

                transform.localPosition = pos;
            }
        }

        private IEnumerator ScaleToBeat(float intensityToReach)
        {
            Vector3 startingScale = transform.localScale;
            float intensity = 0;

            while (intensity < intensityToReach)
            {
                transform.localScale = Vector3.Lerp(startingScale, beatScale, intensity);
                intensity += beatSpeed * Time.deltaTime;

                UpdateLock();

                yield return null;
            }
        }
    }
}
