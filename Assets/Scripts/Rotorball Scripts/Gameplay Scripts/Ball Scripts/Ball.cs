using UnityEngine;

namespace PsychedelicGames.RotorBall.Gameplay
{
    using LevelManagement;
    using Files;

    /// <summary>
    /// Handles the behaviour of a ball including physics, destruction, style, data, etc.
    /// </summary>
    public class Ball : MonoBehaviour
    {
        [SerializeField] private PlayerItems items;
        [SerializeField] private bool usePlayerData = true;
        [Space]
        [SerializeField] private GameObject boostGradient;
        [Space]
        [SerializeField] [Range(0, 1)] private float maxSpeedPercentage;
        [SerializeField] [Range(0, 1)] private float maxSizePercentage;

        private static int ballsInPlay;

        private new Transform transform;
        private new Rigidbody2D rigidbody;
        private new Collider2D collider2D;

        private LevelManager manager;
        private SpriteRenderer sprite;
        private BallTrail trail;
        private BallExplosion explosion;
        private AudioSource source;
        private Slingshot slingshot;

        private float lifeTime;
        private float currentLifeTime;

        private float maxSpeed;
        private float maxSize;

        public float SelfDestructTime { get => lifeTime; set => lifeTime = value; }

        /// Ball Data ///
        private int bricksHit;
        public int BricksHit { get => bricksHit; set => bricksHit = value; }

        private int bricksDestroyed;
        public int BricksDestroyed { get => bricksDestroyed; set => bricksDestroyed = value; }

        private int bonusBricksHit;
        public int BonusBricksHit { get => bonusBricksHit; set => bonusBricksHit = value; }

        private int bonusBricksDestroyed;
        public int BonusBricksDestroyed { get => bonusBricksDestroyed; set => bonusBricksDestroyed = value; }

        public int StandardBricksDestroyed { get; set; }
        public int DurableBricksHit { get; set; }
        public int DurableBricksDestroyed { get; set; }
        public int SwapperBricksDestroyed { get; set; }
        public int ShifterBricksDestroyed { get; set; }
        public int HazardBricksHit { get; set; }
        public int PowerBricksHit { get; set; }

        private int ballScore;
        public int BallScore { get => ballScore; set => ballScore = value; }

        /// Ball Boosts ///
        private bool isScoreSeeker;
        public bool IsScoreSeeker { get => isScoreSeeker; set => isScoreSeeker = value; }

        private float scoreBonus;
        public float ScoreBonus { get => scoreBonus; set => scoreBonus = value; }

        private float charge;
        public float Charge { get => charge; set => charge = value; }

        private bool isChain;
        private float chainRadius;

        private bool isDestroyed;
        private bool inPlay;
        public bool InPlay
        {
            get => inPlay;
        }

        public Vector2 Velocity
        {
            get => rigidbody.velocity;
            set
            {
                rigidbody.velocity = Vector2.ClampMagnitude(value, maxSpeed);
            }
        }

        public void SetAsBoost() => boostGradient.Activate();

        public void EnablePlay(Vector2 vel)
        {
            isDestroyed = false;
            inPlay = true;

            currentLifeTime = lifeTime;
            rigidbody.velocity = vel + (vel * charge);

            SetState(true);

            if (manager)
            {
                manager.BallsInPlay++;
            }
        }

        public void Activate() => gameObject.Activate();
        public void Deactivate() => gameObject.Deactivate();

        public void SetChain(float radius)
        {
            isChain = true;
            chainRadius = radius;
        }

        public void SetScale(float size, bool fromBoost)
        {
            float newSize = transform.localScale.x * size;
            Mathf.Clamp(newSize, 0, maxSize);
            transform.localScale = new Vector3(newSize, newSize, newSize);

            trail.SetWidth(newSize);

            if (fromBoost)
            {
                slingshot.AdjustSize(size, false);
            }
        }

        private void Awake()
        {
            transform = GetComponent<Transform>();

            if (usePlayerData)
            {
                PlayerFile file = PlayerFile.GetFile();

                GameObject styleGO = items.GetStyle(file.CurrentItems[(int)PlayerFile.Items.Style]);
                GameObject trailGO = items.GetTrail(file.CurrentItems[(int)PlayerFile.Items.Trail]);
                GameObject explosionGO = items.GetExplosion(file.CurrentItems[(int)PlayerFile.Items.Explosion]);

                Instantiate(styleGO, transform);
                Instantiate(trailGO, transform);
                Instantiate(explosionGO, transform);
            }

            rigidbody = GetComponent<Rigidbody2D>();
            collider2D = GetComponent<Collider2D>();

            manager = FindObjectOfType<LevelManager>();
            slingshot = FindObjectOfType<Slingshot>();

            sprite = GetComponentInChildren<SpriteRenderer>();
            trail = GetComponentInChildren<BallTrail>();
            explosion = GetComponentInChildren<BallExplosion>();

            collider2D.enabled = false;
            trail.SetState(false);
            trail.ball = transform;

            LevelData data = LevelData.Current;

            if (data)
            {
                LevelModifiers modifiers = data.Modifiers;

                float size = modifiers.BallSize;
                maxSize = size + (size * (1 - maxSizePercentage));

                transform.localScale *= size;

                lifeTime = modifiers.BallLifeTime;

                float speed = modifiers.BallSpeed;
                maxSpeed = speed + (speed * (1 - maxSpeedPercentage));
            }

            enabled = false;
        }

        private void Update()
        {
            if (inPlay && !isDestroyed)
            {
                if ((currentLifeTime -= Time.deltaTime) < 0)
                {
                    Destroy();
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Transform trans = other.transform;
            if (trans.CompareTag("Destructible"))
            {
                if (trans.GetComponentInChildren<Powerup>())
                {
                    currentLifeTime = lifeTime;
                }
                if (isChain)
                {
                    Collider2D[] colliders = new Collider2D[100];
                    int chain = Physics2D.OverlapCircleNonAlloc(trans.position, chainRadius, colliders);

                    for (int i = 0; i < chain; i++)
                    {
                        colliders[i].GetComponent<DestructibleBrick>()?.Destroy(this);
                    }

                    isChain = false;
                }
            }
            else if (trans.CompareTag("Hazard"))
            {
                HazardBricksHit++;
                manager.UpdateHazardStatus();
                Destroy();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("LevelBound")) { Destroy(); }
        }

        private void SetState(bool state)
        {
            collider2D.enabled = state;
            sprite.enabled = state;
            trail.SetState(state);
            enabled = state;
        }

        public void Destroy()
        {
            //Ensure we don't call Destroy() twice
            if (!isDestroyed)
            {
                isDestroyed = true;

                explosion.Explode();
                rigidbody.Sleep();

                SetState(false);
                boostGradient.Deactivate();

                if (manager)
                {
                    if (isScoreSeeker)
                    {
                        int bonus = Mathf.RoundToInt(ballScore * scoreBonus);

                        ballScore += bonus;
                        manager.Score += bonus;
                    }

                    manager.UpdateReport(this);

                    manager.Multiplier -= 1;
                    manager.BallsInPlay--;
                }
            }
        }
    }
}
