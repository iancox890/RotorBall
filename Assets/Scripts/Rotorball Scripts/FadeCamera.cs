using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PsychedelicGames.RotorBall {
    [RequireComponent(typeof(Camera))]
    public class FadeCamera : MonoBehaviour
    {
        [SerializeField] private bool _immediately;
        [SerializeField] private Color _from;
        [SerializeField] private Color _to;
        [SerializeField] private float _time;
        [SerializeField] private UnityEvent _onComplete;
        private float _timer;
        private Camera _camera;
        // Start is called before the first frame update
        private void Awake()
        {
            _camera = GetComponent<Camera>();
            if (_immediately) {
                _camera.backgroundColor = _from;
            }
        }

        private void Start() {
            if (_immediately) {
                Fade();
            }
        }

        private void OnDisable() {
            StopAllCoroutines();
        }

        public void Fade() {
            StartCoroutine(DoFade());
        }

        private IEnumerator DoFade() {
            _timer = _time;
            while (_timer > 0) {
                _camera.backgroundColor = Color.Lerp(_from,_to,1f-_timer/_time);
                yield return new WaitForEndOfFrame();
                _timer -= Time.deltaTime;
            }
            _camera.backgroundColor = _to;
            _onComplete.Invoke();
            yield break;
        }
    }
}