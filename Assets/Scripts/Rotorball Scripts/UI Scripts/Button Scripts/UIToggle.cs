using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.UI
{
    using Audio;
    [RequireComponent(typeof(Toggle))]
    public class UIToggle : MonoBehaviour
    {
        [SerializeField] private AudioClip _selectSound;
        [SerializeField] private AudioClip _deselectSound;

        private AudioSource _source;
        private Toggle _toggle;

        private void Awake() {
            if (AudioManager.Instance) {
                _source = AudioManager.Instance.AudioUI;
            }
            _toggle = GetComponent<Toggle>();
            _toggle.onValueChanged.AddListener(delegate{DoToggle();});
        }

        private void DoToggle() {
            if (_source && _selectSound) { _source.PlayOneShot(_toggle.isOn?_selectSound:_deselectSound); }
            OnToggled();
        }

        protected virtual void OnToggled()
        {
            // print("Test");
        }
    }
}