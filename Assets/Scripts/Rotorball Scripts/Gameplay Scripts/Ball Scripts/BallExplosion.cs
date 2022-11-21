using UnityEngine;

namespace PsychedelicGames.RotorBall.Gameplay
{
    /// <summary>
    /// Handles the ball explosion.
    /// </summary>
    public class BallExplosion : MonoBehaviour
    {
        [SerializeField] private AudioClip[] clips;
        [SerializeField] private ParticleSystem[] particles;

        private AudioSource source;

        public void Explode()
        {
            int clipLength = clips.Length;
            int particleLength = particles.Length;

            for (int i = 0; i < clipLength; i++)
            {
                source.PlayOneShot(clips[i]);
            }
            for (int i = 0; i < particleLength; i++)
            {
                particles[i].Play();
            }
        }

        private void Awake() => source = Audio.AudioManager.Instance.AudioSFX;
    }
}
