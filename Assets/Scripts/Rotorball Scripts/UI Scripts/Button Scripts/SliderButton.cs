using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using PsychedelicGames.RotorBall.Audio;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Sets, loads and saves the value of a slider.
    /// </summary>
    public abstract class SliderButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] protected SliderDirection direction;
        [Space]
        [SerializeField] protected Slider slider;
        [Space]
        [SerializeField] protected float speed;
        [SerializeField] protected float holdDeltaThreshold;

        protected enum SliderDirection { Increase, Decrease }
        
        protected bool isPressed;
        private float holdDelta;

        [SerializeField] protected string key;

        protected delegate void Adjust();
        protected Adjust adjust;

        protected virtual void Save()
        {
            PlayerPrefs.SetFloat(key, slider.normalizedValue);
            PlayerPrefs.Save();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            holdDelta = 0;
            isPressed = true;

            adjust();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isPressed = false;

            Save();
        }

        private void Update()
        {
            if (isPressed)
            {
                holdDelta += Time.deltaTime;
                if (holdDelta > holdDeltaThreshold) { adjust(); }
            }
        }

        protected virtual void Start()
        {
            slider.normalizedValue = PlayerPrefs.GetFloat(key,0.5f);

            switch (direction)
            {
                case SliderDirection.Increase:
                    adjust = () =>
                    {
                        slider.value += speed;
                    };
                    break;
                case SliderDirection.Decrease:
                    adjust = () =>
                    {
                        slider.value -= speed;
                    };
                    break;
            }
        }
    }
}
