using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using PsychedelicGames.RotorBall.Audio;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Sets the game audio volume.
    /// </summary>
    public class VolumeButton : SliderButton
    {
        [SerializeField] private AudioManager.Source source;

        private AudioSource audioSource;
        private AudioManager audioManager;

        protected override void Save()
        {
            audioManager.SaveVolumeSettings(source);
        }

        protected override void Start()
        {
            audioManager = AudioManager.Instance;

            switch (source)
            {
                case AudioManager.Source.UI:
                    audioSource = audioManager.AudioUI;
                    break;
                case AudioManager.Source.Music:
                    audioSource = audioManager.AudioMusic;
                    break;
                case AudioManager.Source.SFX:
                    audioSource = audioManager.AudioSFX;
                    break;
            }

            slider.value = audioManager.GetVolumeSettings(source) * 100;

            switch (direction)
            {
                case SliderDirection.Increase:
                    adjust = () =>
                    {
                        slider.value += speed;
                        audioManager.AdjustVolumeSettings(source, slider.normalizedValue);
                    };
                    break;
                case SliderDirection.Decrease:
                    adjust = () =>
                    {
                        slider.value -= speed;
                        audioManager.AdjustVolumeSettings(source, slider.normalizedValue);
                    };
                    break;
            }
        }
    }
}
