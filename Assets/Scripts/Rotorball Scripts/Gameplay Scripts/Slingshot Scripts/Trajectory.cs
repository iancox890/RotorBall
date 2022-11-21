using UnityEngine;
using System.Collections;

namespace PsychedelicGames.RotorBall.Gameplay
{
    /// <summary>
    /// Draws a trajectory line of the how the ball will bounce.
    /// </summary>
    public class Trajectory : MonoBehaviour
    {
        [SerializeField] private GameObject ballPrefab;
        [SerializeField] private GameObject dotPrefab;
        [Space]
        [SerializeField] private int dotsPerBounce;
        [Space]
        [SerializeField] private Material highlightedDot;
        [SerializeField] private Material dangerDot;
        [Space]
        [SerializeField] private float startingScale;
        [SerializeField] private float scaleDelta;
        [SerializeField] private float opacityDelta;
        [Space]
        [SerializeField] private float fadeSpeed;
        [Space]
        [SerializeField] private CircleCollider2D boundary;

        private const int Bounces = 2;
        private Dot[] dots;
        private IEnumerator routine;
        private Slingshot slingshot;
        private new Transform transform;
        private Vector2 origin = Vector2.zero;
        private Vector2 pos;
        private Vector2 dir;
        private Vector2 zero = Vector2.zero;
        private Color highlightedColour;
        private Color dangerColour;
        private float radius;
        private float boundaryRadius;
        private int totalDots, highlightedDots;
        private bool isVisible;
        private bool isDanger;
        private bool dangerHighlighted;

        private void Awake()
        {
            slingshot = GetComponent<Slingshot>();
            transform = GetComponent<Transform>();

            totalDots = Bounces * dotsPerBounce;
            dots = new Dot[totalDots];

            highlightedColour = highlightedDot.color;
            dangerColour = dangerDot.color;

            radius = ballPrefab.transform.localScale.x * 0.5f;
            boundaryRadius = boundary.radius;

            float opacity = 1;
            float start = startingScale;

            float sclDelta = (scaleDelta / totalDots);
            float opDelta = (opacityDelta / totalDots);

            for (int i = 0; i < totalDots; i++)
            {
                GameObject obj = Instantiate(dotPrefab, transform) as GameObject;
                dots[i] = new Dot(ref obj, opacity -= opDelta);

                Dot dot = dots[i];
                dot.transform.localScale = new Vector2(start, start);

                start -= sclDelta;
            }
            isVisible = true;
        }

        private void OnEnable()
        {
            slingshot.OnLaunched += Hide;
            slingshot.OnDragging += DrawHighlights;
            slingshot.OnReloaded += Show;
            slingshot.OnBallsUpdated += UpdateDisable;
        }

        private void OnDestroy()
        {
            slingshot.OnLaunched -= Hide;
            slingshot.OnDragging -= DrawHighlights;
            slingshot.OnReloaded -= Show;
            slingshot.OnBallsUpdated -= UpdateDisable;
        }

        private void FixedUpdate()
        {
            if (isVisible)
            {
                isDanger = false;

                origin = zero;
                pos = origin;
                dir = transform.up;

                for (int i = 0; i < Bounces; i++)
                {
                    //Cast from the previous hit position
                    RaycastHit2D hit = Physics2D.CircleCast(pos, radius, dir);
                    Collider2D collider = hit.collider;

                    float dotDistance;
                    float dotDelta;

                    if (collider)
                    {
                        if (collider.CompareTag("Hazard"))
                        {
                            isDanger = true;
                        }

                        Vector2 centroid = hit.centroid;
                        pos = hit.point;

                        dotDistance = hit.distance;
                        dotDelta = dotDistance / dotsPerBounce;

                        for (int j = dotsPerBounce - 1; j > -1; j--)
                        {
                            ref Dot temp = ref dots[j + (i * dotsPerBounce)];

                            temp.transform.position = ((centroid - origin).normalized * dotDistance) + origin;
                            temp.gameObject.Activate();

                            dotDistance -= dotDelta;
                        }

                        origin = centroid;
                        dir = Vector2.Reflect(dir, hit.normal);
                    }
                    else
                    {
                        isDanger = true;
                        if (i > 0)
                        {
                            dotDistance = boundaryRadius - Mathf.Abs(origin.y);
                        }
                        else
                        {
                            dotDistance = boundaryRadius;
                        }
                        dotDelta = dotDistance / dotsPerBounce;

                        for (int j = dotsPerBounce - 1; j > -1; j--)
                        {
                            ref Dot temp = ref dots[j + (i * dotsPerBounce)];

                            temp.transform.position = (dir * dotDistance) + origin;
                            temp.gameObject.Activate();

                            dotDistance -= dotDelta;
                        }
                    }
                }
                if (isDanger)
                {
                    DrawDanger();
                }
                else
                {
                    Erase();
                }
            }
        }

        private void UpdateDisable(int count)
        {
            //Check to see if we are out of balls... If so, disable
            if (count <= 0)
            {
                enabled = false;
            }
            else
            {
                if (!enabled)
                {
                    enabled = true;
                }
            }
        }

        private void FadeDots(float from, float to)
        {
            //Only start if we're active...
            if (enabled)
            {
                //Prevent multiple coroutines from running...
                if (routine != null)
                {
                    StopCoroutine(routine);
                }

                routine = Routine();
                StartCoroutine(routine);

                IEnumerator Routine()
                {
                    float percent = 0;
                    while (percent != 1)
                    {
                        percent = percent > 1 ? 1 : percent + (fadeSpeed * Time.deltaTime);
                        float opacity = Mathf.Lerp(from, to, percent);

                        for (int i = 0; i < totalDots; i++) { dots[i].SetOpacity(opacity); }

                        yield return null;
                    }
                }
            }
        }

        private void DrawDanger()
        {
            if (dangerHighlighted == false)
            {
                for (int i = 0; i < totalDots; i++)
                {
                    dots[i].SetColour(dangerColour);
                }
                dangerHighlighted = true;
            }
        }

        private void Erase()
        {
            if (dangerHighlighted)
            {
                for (int i = 0; i < totalDots; i++)
                {
                    dots[i].ResetColour();
                }
                dangerHighlighted = false;
            }
        }

        private void DrawHighlights(float percentage)
        {
            //Reveal dots based on whether or not they're within the power percentage
            int highlighted = Mathf.RoundToInt(percentage * totalDots);

            if (highlighted == highlightedDots) { return; }
            highlightedDots = highlighted;

            for (int i = 0; i < totalDots; i++)
            {
                if (i < highlighted) { dots[i].SetColour(highlightedColour); }
                else if (isDanger) { dots[i].SetColour(dangerColour); }
                else { dots[i].ResetColour(); }
            }
        }

        private void Show()
        {
            isVisible = true;

            for (int i = 0; i < totalDots; i++) { dots[i].ResetColour(); }
            FadeDots(0, 1);
        }

        private void Hide()
        {
            isVisible = false;

            for (int i = 0; i < totalDots; i++) { dots[i].ResetColour(); }
            FadeDots(1, 0);
        }
    }

    internal struct Dot
    {
        internal GameObject gameObject;
        internal Material material;
        internal Transform transform;
        internal Color defaultColour;
        internal float opacity;

        internal Dot(ref GameObject gameObject, float opacity)
        {
            this.gameObject = gameObject;
            this.opacity = opacity;

            material = this.gameObject.GetComponent<Renderer>().material;
            transform = this.gameObject.transform;

            defaultColour = material.color;
            defaultColour.a = opacity;

            material.color = defaultColour;
        }

        internal void SetOpacity(float p)
        {
            var colour = material.color;
            colour.a = opacity * p;

            material.color = colour;
        }

        internal void SetColour(Color colour)
        {
            colour.a = material.color.a;
            material.color = colour;
        }

        internal void ResetColour() => material.color = defaultColour;
    }
}
