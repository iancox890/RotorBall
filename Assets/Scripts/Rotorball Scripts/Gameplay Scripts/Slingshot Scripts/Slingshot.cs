using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

namespace PsychedelicGames.RotorBall.Gameplay
{
    using LevelManagement;
    using Files;

    /// <summary>
    /// Controls the slingshot for a given level.
    /// </summary>
    public class Slingshot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private bool infiniteBalls = false;
        [Space]
        [SerializeField] private LineRenderer ropeLine;
        [Space]
        [SerializeField] private Transform rope;
        [SerializeField] private Transform cup;
        [SerializeField] private Transform main;
        [Space]
        [SerializeField] private AnimationCurve wobbleCurve;
        [SerializeField] private float wobbleSpeed;
        [Space]
        [SerializeField] private float maxDrag;
        [SerializeField] private float dragArea;
        [Space]
        [SerializeField] private GameObject ballPrefab;

        private const float MinLaunchPercent = 0.01f;

        private new Camera camera;
        private new Transform transform;

        private Scaler scaler;

        [SerializeField] private Rotor rotor;
        private Ball[] balls;

        private IEnumerator currentWobble;

        private Vector3 zero = Vector3.zero;

        private int ballCount;
        private int extraBallCount;
        private int currentBallCount;
        private int ballsLaunched;

        private float origin;
        private float max;
        private float powerPercent;
        private float maxPower;
        private float sizeMultiplier;
        private float defaultWidth;

        private bool useExtraBalls;
        private bool reloaded = true;

        public Ball CurrentBall
        {
            get
            {
                if (HasBalls)
                {
                    return GetBall();
                }
                return null;
            }
        }

        public bool HasBalls { get => currentBallCount > 0 || infiniteBalls; }
        public bool IsDragging { get; set; }

        public int Balls { get => currentBallCount; }

        public event Action OnLaunched;
        public event Action<float> OnDragging;
        public event Action OnReloaded;
        public event Action<int> OnBallsUpdated;

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (HasBalls)
            {
                origin = camera.ScreenToViewportPoint(eventData.pressPosition).y;
                rotor.IsRotatable = false;
            }
        }

        private Ball currentBall;

        public void OnDrag(PointerEventData eventData)
        {
            IsDragging = true;

            //Drag the sling if we have more balls
            if (HasBalls)
            {
                float viewPort = Mathf.Clamp(camera.ScreenToViewportPoint(eventData.position).y - origin, -1, 0);
                float y = Mathf.Clamp(max * -(viewPort * dragArea), max, 0);

                Vector2 pos = new Vector2(0, y);
                ropeLine.SetPosition(1, pos);

                cup.localPosition = pos;
                GetBall().transform.localPosition = pos * main.localScale;

                powerPercent = Mathf.Abs(y / max);

                OnDragging?.Invoke(powerPercent);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            IsDragging = false;

            if (HasBalls) {
                Ball ball = GetBall();
                //Launch the sling
                if (reloaded)
                {
                    // Ball ball = GetBall();

                    if (powerPercent > MinLaunchPercent)
                    {
                        ball.EnablePlay((powerPercent * maxPower) * transform.up);
                        ball.transform.SetParent(null);

                        if (!infiniteBalls)
                            {currentBallCount--;}
                        reloaded = false;

                        OnLaunched?.Invoke();
                        OnBallsUpdated?.Invoke(currentBallCount);

                        if (currentWobble != null)
                        {
                            StopCoroutine(currentWobble);
                        }
                        currentWobble = Wobble();
                        StartCoroutine(currentWobble);

                        ballsLaunched++;
                    } else {
                        ball.transform.position = Vector3.zero;
                    }
                } else {
                    ball.transform.position = Vector3.zero;
                }
            }
            
            rotor.IsRotatable = true;
        }

        public void GiveBalls()
        {
            currentBallCount = extraBallCount;
            useExtraBalls = true;

            Reload();

            OnBallsUpdated?.Invoke(extraBallCount);
        }

        public void AdjustSize(float value, bool reset)
        {
            Vector3 mainScale;
            if (reset)
            {
                mainScale = Vector3.one;
                rope.localScale = Vector3.one;

                ropeLine.widthMultiplier = defaultWidth;
                max = -maxDrag;
            }
            else
            {
                mainScale = main.localScale;
                ropeLine.widthMultiplier *= value;
                max /= value;
            }

            scaler.Scale(main, main.localScale, mainScale * value);
            rope.localScale *= value;
        }

        private void Awake()
        {
            transform = GetComponent<Transform>();
            camera = Camera.main;

            scaler = GetComponent<Scaler>();

            rotor = rotor??FindObjectOfType<Rotor>();

            defaultWidth = ropeLine.widthMultiplier;

            LevelData data = LevelData.Current;

            if (data)
            {
                LevelModifiers modifiers = data.Modifiers;

                ballCount = modifiers.BallCount;
                extraBallCount = modifiers.ExtraBallCount;
                sizeMultiplier = modifiers.BallSize;
                maxPower = modifiers.BallSpeed;

                AdjustSize(sizeMultiplier, false);
            }
            else
            {
                ballCount = 5;
            }

            Transform slingBalls = transform.GetChild(0);

            balls = new Ball[ballCount + extraBallCount];
            currentBallCount = ballCount;

            OnBallsUpdated?.Invoke(ballCount);

            if (!infiniteBalls)
            {
                for (int i = 0; i < ballCount + extraBallCount; i++)
                {
                    Ball temp = (Instantiate(ballPrefab, slingBalls) as GameObject).GetComponent<Ball>();
                    temp.Deactivate();

                    balls[i] = temp;
                }
            }

            currentBall = GetBall();
            currentBall.Activate();
            max = -(maxDrag / sizeMultiplier);
        }

        private void OnDisable()
        {
            StatisticsFile statisticsFile = StatisticsFile.File;
            statisticsFile.BallsLaunched += ballsLaunched;
            StatisticsFile.File = statisticsFile;
        }

        private Ball GetBall()
        {
            if (infiniteBalls)
            {
                if (currentBall == null || currentBall.InPlay)
                {
                    Transform slingBalls = transform.GetChild(0);
                    currentBall = (Instantiate(ballPrefab, slingBalls) as GameObject).GetComponent<Ball>();
                }
                return currentBall;
            }
            if (!useExtraBalls)
            {
                return balls[currentBallCount - 1];
            }
            else
            {
                return balls[ballCount + (currentBallCount - 1)];
            }
        }

        private void Reload()
        {
            GetBall().Activate();
            reloaded = true;
            AdjustSize(sizeMultiplier, true);
            OnReloaded?.Invoke();
        }

        private IEnumerator Wobble()
        {
            Vector2 newPos = Vector2.zero;

            float percent = 0;
            float y = ropeLine.GetPosition(1).y;

            //make it reach 100 percent fully
            while (percent != 1)
            {
                percent = percent > 1 ? 1 : percent + (wobbleSpeed * Time.deltaTime);

                float curvePercent = wobbleCurve.Evaluate(percent);
                newPos.Set(0, Mathf.LerpUnclamped(0, y, curvePercent));

                ropeLine.SetPosition(1, newPos);
                cup.localPosition = newPos;

                yield return null;
            }

            if (HasBalls)
            {
                Reload();
            }
        }
    }
}
