using UnityEngine;
using PsychedelicGames.RotorBall.Audio;

namespace PsychedelicGames.RotorBall.Gameplay
{
    /// <summary>
    /// Triggers audio for a given brick.
    /// </summary>
    public class BrickAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip[] hitClips;
        [SerializeField] private AudioClip[] finalHitClips;
        [SerializeField] private AudioClip bonusClip;
        [SerializeField] private bool isDurable;

        private AudioSource source;
        private Scoring scoring;
        private DestructibleBrick destructible;

        private int hitLength;
        private int finalHitLength;

        private void Start()
        {
            source = AudioManager.Instance.AudioSFX;
            scoring = GetComponent<Scoring>();

            hitLength = hitClips.Length;
            finalHitLength = finalHitClips.Length;

            if (isDurable)
            {
                destructible = GetComponent<DestructibleBrick>();
                destructible.OnBrickDestroyed += PlayFinalHit;
            }
        }

        private void OnDisable()
        {
            if (isDurable && destructible)
            {
                destructible.OnBrickDestroyed -= PlayFinalHit;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.CompareTag("Player"))
            {
                for (int i = 0; i < hitLength; i++)
                {
                    source?.PlayOneShot(hitClips[i],0.5f);
                }
                PlayBonusClipIfApplicable();
            }
        }

        private void PlayFinalHit()
        {
            for (int i = 0; i < finalHitLength; i++)
            {
                source?.PlayOneShot(finalHitClips[i]);
            }
            PlayBonusClipIfApplicable();
        }

        private void PlayBonusClipIfApplicable()
        {
            if (scoring.IsBonus)
            {
                source?.PlayOneShot(bonusClip);
            }
        }
    }
}
