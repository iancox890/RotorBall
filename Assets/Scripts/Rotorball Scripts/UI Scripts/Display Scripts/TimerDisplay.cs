using UnityEngine;
using UnityEngine.UI;
using PsychedelicGames.RotorBall.Gameplay;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Displays the time of play in the gameplay UI.
    /// </summary>
    public class TimerDisplay : MonoBehaviour
    {
        [SerializeField] private bool _debugAlwaysFlash = false;
        [SerializeField] private string _popupMessage = "Failed Objective";
        [SerializeField] [Range(0f,4f)] private float _popupTime = 2f;
        [SerializeField] [Range(0,9)] private int _popupFlashSpeed = 7;
        [SerializeField] private Text _timeText;

        private Slingshot _slingshot;
        private float _time;
        LevelManagement.LevelManager _levelManager;

        public float Gametime { get => _time; }

        private float _target;

        private float _startTime;

        private void Awake()
        {
            enabled = false;
            _slingshot = FindObjectOfType<Slingshot>();
            _slingshot.OnLaunched += StartUpdate;
            _levelManager = FindObjectOfType<LevelManagement.LevelManager>();
            _target = Mathf.Infinity;
            foreach (Objectives.Objective o in _levelManager.GetLevel().Objectives)
            {
                if (o is Objectives.TimeLimit)
                {
                    _target = ((Objectives.TimeLimit)o).Time;
                }
            }
        }

        private void OnDestroy() => _slingshot.OnLaunched -= StartUpdate;

        private void Update()
        {
            // _time = 
            if ((_time >= _target && _time < _target + _popupTime) || _debugAlwaysFlash)
            {
                bool showPopup = (Mathf.FloorToInt(_time*10f/_popupFlashSpeed) % 2) == 0;
                _timeText.text = showPopup ? "" : _popupMessage;
            }
            else
            {
                _timeText.text = _time.ToFormattedTime();
            }
            _time += Time.deltaTime;
            _levelManager.levelTime = _time;
        }

        private void StartUpdate()
        {
            enabled = true;
            _startTime = Time.time;
            _slingshot.OnLaunched -= StartUpdate;
        }
    }
}