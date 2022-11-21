using UnityEngine;
using System.Collections;

namespace PsychedelicGames.RotorBall.Gameplay
{
    /// <summary>
    /// Shifts between a destructible and indestructible state.
    /// </summary>
    public class Shifter : MonoBehaviour
    {
        [SerializeField]
        private float initialInterval, mainInterval, hollowInterval, fadeSpeed;

        private IEnumerator routine;
        private SpriteRenderer brickRenderer;
        private Collider2D brickCollider;
        private IntervalTimer timer;
        private Color colour;

        private bool shift = true;

        private void Awake()
        {
            brickRenderer = GetComponent<SpriteRenderer>();
            brickCollider = GetComponent<Collider2D>();

            timer = GetComponent<IntervalTimer>();

            timer.Interval = initialInterval;
        }

        private void OnEnable() => timer.OnIntervalReached += Shift;
        private void OnDisable() => timer.OnIntervalReached -= Shift;

        private void Shift()
        {
            if (shift)
            {
                FadeShifter(1, 0);
                timer.Interval = hollowInterval;
            }
            else
            {
                FadeShifter(0, 1);
                timer.Interval = mainInterval;
            }

            shift = !shift;
            brickCollider.enabled = shift;
        }

        private void FadeShifter(float from, float to)
        {
            //Prevent multiple coroutines from running...
            if (routine != null) { StopCoroutine(routine); }

            Material mat = brickRenderer.material;
            colour = mat.color;

            routine = Routine();

            StartCoroutine(routine);

            IEnumerator Routine()
            {
                float percent = 0;

                while (percent != 1)
                {
                    percent = percent > 1 ? 1 : percent + (fadeSpeed * Time.deltaTime);
                    colour.a = Mathf.Lerp(from, to, percent);
                    mat.color = colour;

                    yield return null;
                }

                brickCollider.enabled = shift;
            }
        }
    }
}
