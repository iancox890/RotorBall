using UnityEngine;

namespace PsychedelicGames.RotorBall.LevelManagement
{
    /// <summary>
    /// Holds the modifiers for a given level.
    /// </summary>
    [CreateAssetMenu(fileName = "Level Modifiers.asset", menuName = "Psychedelic Games/Levels/Level Modifiers")]
    public class LevelModifiers : ScriptableObject
    {
        [SerializeField] private int brickCount;
        [SerializeField] private int ballCount;
        [SerializeField] private int extraBallCount = 3;
        [SerializeField] private float ballLifeTime = 4;
        [SerializeField] private float ballSpeed;
        [SerializeField] private float ballSize;
        [SerializeField] private GameScene designatedScene;

        public int BrickCount { get => brickCount; }
        public int BallCount { get => ballCount; }
        public int ExtraBallCount { get => extraBallCount; }

        public float BallLifeTime { get => ballLifeTime; }
        public float BallSpeed { get => ballSpeed; }
        public float BallSize { get => ballSize; }
    }
}
