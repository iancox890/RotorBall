using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class ResizeToSafeArea : MonoBehaviour
    {
        [Tooltip("Should always be false!")] [SerializeField] private bool _debugShouldBeFalse = false;
        // [SerializeField] [Range(0f,0.1f)] private float _notch_size = 0f;
        // [SerializeField] private float _positionOffset;
        [SerializeField] [Range(8f,88f)] private float _notchSize;
        [SerializeField] RectTransform _rectTransform;

        private bool _resized = false;
        // private Rect _safeArea;

        private void OnValidate() {
            if (enabled && _rectTransform != null)
            {
                Resize();
            }
        }
        void Start()
        {
            _rectTransform = _rectTransform ?? GetComponent<RectTransform>();
            Resize();
        }

        void Resize()
        {
            if (_rectTransform == null) { _rectTransform = GetComponent<RectTransform>(); }
            if ((!_resized || _debugShouldBeFalse) && _rectTransform != null)
            {
                _resized = true;
                Rect safeArea;
                #if UNITY_EDITOR
                    if (_debugShouldBeFalse)
                    {
                        // safeArea = new Rect(Screen.safeArea.position.x,Screen.safeArea.position.y - (Screen.height*_notch_size),Screen.safeArea.size.x,Screen.safeArea.size.y * (1f-_notch_size));
                        safeArea = new Rect(Screen.safeArea.position.x,Screen.safeArea.position.y,Screen.safeArea.size.x,Screen.safeArea.size.y - _notchSize);
                    }
                    else
                    {
                        safeArea = Screen.safeArea;
                    }
                #else
                    safeArea = Screen.safeArea;
                #endif

                _rectTransform = _rectTransform ?? GetComponent<RectTransform>();
                Vector2 anchorMin = safeArea.position;
                Vector2 anchorMax = safeArea.position + safeArea.size;
                anchorMin.x /= Screen.width;
                anchorMin.y /= Screen.height;
                anchorMax.x /= Screen.width;
                anchorMax.y /= Screen.height;
                _rectTransform.anchorMin = anchorMin;
                _rectTransform.anchorMax = anchorMax;
            }
        }
    }
}