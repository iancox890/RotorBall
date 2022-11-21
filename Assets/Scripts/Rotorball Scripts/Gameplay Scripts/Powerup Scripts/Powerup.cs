using UnityEngine;

namespace PsychedelicGames.RotorBall.Gameplay
{
    /// <summary>
    /// Base class for a given powerup.
    /// </summary>
    public abstract class Powerup : MonoBehaviour
    {
        protected DestructibleBrick sourceBrick;

        private void Awake()
        {
            sourceBrick = GetComponent<DestructibleBrick>();
            Init();
        }

        private void OnEnable() => sourceBrick.OnBrickDestroyed += Activate;
        private void OnDisable() => sourceBrick.OnBrickDestroyed -= Activate;

        protected abstract void Activate();
        protected virtual void Init() { }
    }
}
