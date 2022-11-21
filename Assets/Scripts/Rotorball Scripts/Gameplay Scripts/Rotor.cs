using UnityEngine;
using UnityEngine.EventSystems;

namespace PsychedelicGames.RotorBall.Gameplay
{
    using UI;
    using Files;

    /// <summary>
    /// Handles the rotation for a level.
    /// </summary>
    public class Rotor : MonoBehaviour
    {
        private float sensitivity;
        [SerializeField] [Range(0.5f,2f)] private float _sensitivityMultiplier = 2f;
        [SerializeField] [Range(0f,1f)] private float _sensitivityOffset = 0.08f;
        [SerializeField] [Range(0, 1)] private float dampen;
        private bool invertControls;
        [Space]
        // [SerializeField] private UIButton boostButton;

        // private delegate void UpdateRotor();
        // private UpdateRotor update;

        private Transform cameraTransform;
        private Transform sling;
        private Camera cam;
        private EventSystem eventSystem;
        // private Touch touch;

        private float deltaRotation;
        private float previousRotation;
        private float currentRotation;

        private float _totalRotation;

        private bool isDown;
        private bool isRotatable;
        public bool IsRotatable
        {
            get => isRotatable;
            set
            {
                if (value == false) { deltaRotation = 0; }
                isRotatable = value;
            }
        }

        public void LoadControlSettings()
        {
            float saved = PlayerPrefs.GetFloat("CONTROLS_SENSITIVITY", 0.5f);
            sensitivity = (saved != 0f ? saved : 0.01f)*_sensitivityMultiplier + _sensitivityOffset;
            invertControls = PlayerPrefs.GetInt("CONTROLS_INVERT", 0)==0?false:true;
        }

        private void Start()
        {
            LoadControlSettings();

            cam = Camera.main;

            sling = GameObject.FindGameObjectWithTag("Slingshot").transform;
            cameraTransform = cam.transform;
            eventSystem = EventSystem.current;

            //boostButton.Button.onClick.AddListener(UpdateRotation);

            isRotatable = true;
        }

        // private void UpdateRotation() => isRotatable = !isRotatable;

        // TODO: Decypher magic function
        private float GetAngleFromPoint(Vector2 point)
        {
            Vector2 centre = Vector2.one;

            float angle = Vector2.Angle(point, centre);
            
            if (Vector3.Cross(point, centre).z > 0)
            {
                angle = 360f - angle;
            }

            return angle;
        }
        
        private bool IsRotatableFromPointer() => eventSystem ? !eventSystem.IsPointerOverGameObject() : true;
        private bool IsRotatableFromPointer(int pointerID) => eventSystem ? !eventSystem.IsPointerOverGameObject(pointerID) : true;

        private void Update() {
            if (isRotatable)
            {
                // Time between this frame and previous
                float deltaTime = Time.deltaTime;
                Vector2 position = Vector3.zero;
                bool justStartedDrag = false;
                bool alreadyDragging = false;
                bool rotatable = false;

                #if UNITY_EDITOR
                    position = Input.mousePosition;
                    justStartedDrag = Input.GetMouseButtonDown(0);
                    alreadyDragging = Input.GetMouseButton(0);
                    rotatable = IsRotatableFromPointer();
                #elif UNITY_ANDROID || UNITY_IOS
                    if (Input.touchCount > 0) {
                        Touch touch = Input.GetTouch(0);
                        position = touch.position;
                        justStartedDrag = touch.phase == TouchPhase.Began;
                        alreadyDragging = touch.phase != TouchPhase.Ended;
                        rotatable = IsRotatableFromPointer(touch.fingerId);
                    }
                #endif

                if (justStartedDrag && rotatable) {
                    deltaRotation = 0;
                    previousRotation = GetAngleFromPoint(cam.ScreenToWorldPoint(position));

                    isDown = true;
                } else if (alreadyDragging && rotatable && isDown) {
                    currentRotation = GetAngleFromPoint(cam.ScreenToWorldPoint(Input.mousePosition));
                    deltaRotation = Mathf.DeltaAngle(currentRotation, previousRotation) * (invertControls?1:-1);
                    previousRotation = currentRotation;
                } else {
                    deltaRotation = Mathf.Lerp(deltaRotation, 0, deltaTime) * dampen;
                    previousRotation = deltaRotation;

                    isDown = false;
                }

                // Do the rotating
                Vector3 axis = Vector3.back;
                float angle = deltaRotation * sensitivity * Time.deltaTime * 20f;
                sling.Rotate(axis, angle);
                cameraTransform.Rotate(axis, angle);
                _totalRotation += angle;
                if (_totalRotation >= 2f*Mathf.PI) {
                    _totalRotation -= 2f*Mathf.PI;
                    _totalRotations++;
                } else if (_totalRotation < 0) {
                    _totalRotation += 2f*Mathf.PI;
                    _totalRotations++;
                }
            }
        }

        private uint _totalRotations;

        private void OnDestroy() {
            StatisticsFile statisticsFile = StatisticsFile.File;
            statisticsFile.TotalRotations += _totalRotations;
            StatisticsFile.File = statisticsFile;
        }
    }
}
