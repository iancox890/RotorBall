using UnityEngine;

namespace PsychedelicGames.RotorBall.Gameplay
{
    /// <summary>
    /// Sets the trail renderer's width via a multiplier in accordance
    /// with the ball's scale.
    /// </summary>
    public class TrailWidth : MonoBehaviour
    {
        [SerializeField] private float multiplier = 1;

        private BallTrail ballTrail;
        private TrailRenderer trailRenderer;

        private void Awake()
        {
            ballTrail = GetComponentInParent<BallTrail>();
            trailRenderer = GetComponent<TrailRenderer>();
        }

        private void Start() => SetWidth(ballTrail.ball.localScale.x);

        private void OnEnable() => ballTrail.OnWidthAdjusted += SetWidth;
        private void OnDisable() => ballTrail.OnWidthAdjusted -= SetWidth;

        private void SetWidth(float width) => trailRenderer.widthMultiplier = width * multiplier;
    }
}
