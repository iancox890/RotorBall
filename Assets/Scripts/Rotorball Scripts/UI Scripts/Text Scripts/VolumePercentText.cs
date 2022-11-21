using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Sets the percent text based off the value of the volume slider.
    /// </summary>
    public  class VolumePercentText : MonoBehaviour
    {
        [SerializeField] private Slider volumeSlider;
        private Text volumeText;

        private void Awake()
        {
            volumeText = GetComponent<Text>();
            SetText(volumeSlider.value);
        }

        private void OnEnable() => volumeSlider.onValueChanged.AddListener(SetText);
        private void OnDisable() => volumeSlider.onValueChanged.RemoveListener(SetText);

        private void SetText(float value) => volumeText.text = value + "%";
    }
}
