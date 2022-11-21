using UnityEngine;

namespace PsychedelicGames.RotorBall.Audio
{
    /// <summary>
    /// Sets whichever game object this component is attached to as a singleton.
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private AudioSource audioUI;
        [SerializeField] private AudioSource audioMusic;
        [SerializeField] private AudioSource audioSFX;

        private const int Length = 256;
        private static AudioManager instance = null;
        private static float bassValue;

        private float[] samples = new float[Length];

        private string prefUI = "Audio UI";
        private string prefMusic = "Audio Music";
        private string prefSFX = "Audio SFX";

        public enum Source { UI, Music, SFX }

        public static AudioManager Instance { get => instance; }
        public static float BassValue { get => bassValue; }

        public AudioSource AudioUI { get => audioUI; }
        public AudioSource AudioMusic { get => audioMusic; }
        public AudioSource AudioSFX { get => audioSFX; }

        public void AdjustVolumeSettings(Source source, float value)
        {
            switch (source)
            {
                case AudioManager.Source.UI:
                    audioUI.volume = value;
                    break;
                case AudioManager.Source.Music:
                    audioMusic.volume = value;
                    break;
                case AudioManager.Source.SFX:
                    audioSFX.volume = value;
                    break;
            }
        }

        public void SaveVolumeSettings(Source source)
        {
            switch (source)
            {
                case AudioManager.Source.UI:
                    PlayerPrefs.SetFloat(prefUI, audioUI.volume);
                    break;
                case AudioManager.Source.Music:
                    PlayerPrefs.SetFloat(prefMusic, audioMusic.volume);
                    break;
                case AudioManager.Source.SFX:
                    PlayerPrefs.SetFloat(prefSFX, audioSFX.volume);
                    break;
            }
            PlayerPrefs.Save();
        }

        public float GetVolumeSettings(Source source)
        {
            switch (source)
            {
                case AudioManager.Source.UI:
                    return PlayerPrefs.GetFloat(prefUI, 0.3f);
                case AudioManager.Source.Music:
                    return PlayerPrefs.GetFloat(prefMusic, 0.7f);
                case AudioManager.Source.SFX:
                    return PlayerPrefs.GetFloat(prefSFX, 0.4f);
                default:
                    return 1;
            }
        }

        private void Awake()
        {
            transform.SetParent(null);
            if (instance == null)
            {
                instance = this;

                audioUI.volume = PlayerPrefs.HasKey(prefUI) ? PlayerPrefs.GetFloat(prefUI) : 1;
                audioMusic.volume = PlayerPrefs.HasKey(prefMusic) ? PlayerPrefs.GetFloat(prefMusic) : 1;
                audioSFX.volume = PlayerPrefs.HasKey(prefSFX) ? PlayerPrefs.GetFloat(prefSFX) : 1;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(this);
        }

        private void Update()
        {
            audioMusic.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);
            bassValue = (samples[0] + samples[1] + samples[2]) / 3;
        }
    }
}
