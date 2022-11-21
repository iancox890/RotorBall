using UnityEngine;
using System.Collections;

namespace PsychedelicGames.RotorBall.Gameplay
{
    /// <summary>
    /// Scales a given transform according to given parameters.
    /// </summary>
    public class Scaler : MonoBehaviour
    {
        [SerializeField] private float scaleSpeed = 6;

        private IEnumerator routine;
        public delegate void Scaled();

        public void Scale(Transform transform, Vector3 from, Vector3 to)
        {
            if (isActiveAndEnabled)
            {
                float percent = 0;

                routine = Routine();
                StartCoroutine(routine);

                IEnumerator Routine()
                {
                    do
                    {
                        transform.localScale = Vector3.Lerp(from, to, percent);
                        percent = percent > 1 ? 0 : percent + (scaleSpeed * Time.deltaTime);

                        yield return null;
                    }

                    while (percent != 0);
                }
            }
        }

        public void Scale(Transform transform, Vector3 from, Vector3 to, Scaled onScaled)
        {
            float percent = 0;

            routine = Routine();
            StartCoroutine(routine);

            IEnumerator Routine()
            {
                do
                {
                    transform.localScale = Vector3.Lerp(from, to, percent);
                    percent = percent > 1 ? 0 : percent + (scaleSpeed * Time.deltaTime);

                    yield return null;
                }

                while (percent != 0);
                onScaled?.Invoke();
            }
        }
    }
}
