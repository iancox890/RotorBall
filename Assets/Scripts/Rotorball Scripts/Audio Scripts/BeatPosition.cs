using UnityEngine;
using System.Collections;

namespace PsychedelicGames.RotorBall.Audio
{
    /// <summary>
    /// Adjusts an objects position based on a beat.
    /// </summary>
    public class BeatPosition : MonoBehaviour, IBeat
    {
        [SerializeField] private Vector3 restPos;
        [SerializeField] private Vector3 beatPos;
        [Space]
        [SerializeField] private float restSpeed = 5;
        [SerializeField] private float beatSpeed = 8;
        [Space]
        [SerializeField] private float intensityModifier = 8;

        private new Transform transform;

        private IEnumerator moveToBeat;

        public void OnBeat(float deltaTime, float intensity)
        {
            if (moveToBeat != null) { StopCoroutine(moveToBeat); }
            moveToBeat = MoveToBeat(intensity * intensityModifier);
            StartCoroutine(moveToBeat);
        }

        public void OffBeat(float deltaTime) => transform.localPosition = Vector3.Lerp(transform.localPosition, restPos, deltaTime * restSpeed);

        private void Start()
        {
            transform = GetComponent<Transform>();
            transform.localPosition = restPos;
        }

        private IEnumerator MoveToBeat(float intensityToReach)
        {
            Vector3 startingPosition = transform.localPosition;
            float intensity = 0;

            while (intensity < intensityToReach)
            {
                transform.localPosition = Vector3.Lerp(startingPosition, beatPos, intensity);
                intensity += beatSpeed * Time.deltaTime;

                yield return null;
            }
        }
    }
}
