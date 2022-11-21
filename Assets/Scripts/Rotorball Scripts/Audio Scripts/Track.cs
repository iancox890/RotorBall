using UnityEngine;

namespace PsychedelicGames.RotorBall.Audio
{
    /// <summary>
    /// Represents an audio track.
    /// </summary>
    [CreateAssetMenu(fileName = "Track.asset", menuName = "Psychedelic Games/Audio/Track")]
    public class Track : ScriptableObject
    {
        [SerializeField] private AudioClip clip;
        [Space]
        [SerializeField] private string artist;
        [SerializeField] private string song;
        [SerializeField] private string recordLabel;

        public AudioClip Clip { get => clip; }

        public string Artist { get => artist; }
        public string Song { get => song; }
        public string RecordLabel { get => recordLabel; }
    }
}
