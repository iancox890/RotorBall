using UnityEngine;

namespace PsychedelicGames.RotorBall.Gameplay
{
    /// <summary>
    /// Destroys all bricks within a certain radius once activated.
    /// </summary>
    public class Chain : Powerup
    {
        [SerializeField] private int maxDestructionAmount;
        [SerializeField] private float destructionRadius;

        private Collider2D[] colliders;

        protected override void Init() => colliders = new Collider2D[maxDestructionAmount];

        protected override void Activate()
        {
            Physics2D.OverlapCircleNonAlloc(transform.position, destructionRadius, colliders);
            for (int i = 0; i < colliders.Length; i++) { colliders[i].GetComponent<DestructibleBrick>()?.Destroy(sourceBrick.SourceBall); }
        }
    }
}
