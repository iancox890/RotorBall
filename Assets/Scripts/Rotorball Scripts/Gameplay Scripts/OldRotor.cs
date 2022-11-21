using UnityEngine;
using UnityEngine.EventSystems;

namespace PsychedelicGames.RotorBall.Gameplay
{
    /// <summary>
    /// Handles the rotation for a level.
    /// </summary>
    public class OldRotor : MonoBehaviour
    {
        [SerializeField] private float maxDefaultSpeed;
        [SerializeField] private float maxPrecisionSpeed;
        [Space]
        [SerializeField] private float precisionOffset;
        [SerializeField] [Range(0, 1)] private float zoomPercentage;
        [Space]
        [SerializeField] private float transitionTime;

        private delegate void UpdateRotor();
        private UpdateRotor update;

        private Transform sling;
        private EventSystem eventSystem;
        private new Camera camera;
        private Transform cameraTransform;
        private Transform cameraParent;
        private Touch touch;

        private Vector3 cameraVel = Vector3.zero;
        private float sizeVel = 0;

        private Vector3 originalPos;
        private Vector3 precisionPos;
        private Vector3 desiredPos;

        private float viewSize;
        private float precisionViewSize;
        private float desiredViewSize;

        private float offset = 0.5f;
        private float speed;
        private float vel;

        public bool IsRotatable { get; set; }

        private void Start()
        {
            sling = GameObject.FindGameObjectWithTag("Slingshot").transform;

            eventSystem = EventSystem.current;
            camera = Camera.main;
            cameraTransform = camera.transform;
            cameraParent = cameraTransform.parent;

            viewSize = camera.orthographicSize;
            precisionViewSize = viewSize * zoomPercentage;

            originalPos = cameraTransform.position;
            precisionPos = new Vector3(0, precisionOffset, originalPos.z);

            SetCameraFocus(viewSize, originalPos);

            IsRotatable = true;

#if UNITY_EDITOR
            update = EditorUpdate;
#elif UNITY_ANDROID || UNITY_IOS
            update = MobileUpdate;
#endif
        }

        private void Update()
        {
            if (IsRotatable)
            {
                update();
            }
        }

        private void LateUpdate()
        {
            cameraTransform.localPosition = Vector3.SmoothDamp(cameraTransform.localPosition, desiredPos, ref cameraVel, transitionTime);
            camera.orthographicSize = Mathf.SmoothDamp(camera.orthographicSize, desiredViewSize, ref sizeVel, transitionTime);
        }

        private void SetCameraFocus(float size, Vector3 pos)
        {
            desiredViewSize = size;
            desiredPos = pos;
        }

        private void Rotate(float x, float y)
        {
            //Rotate in a direction given by the position of input
            if (y < offset)
            {
                speed = maxDefaultSpeed;
                SetCameraFocus(viewSize, originalPos);
            }
            else
            {
                speed = maxPrecisionSpeed;
                SetCameraFocus(precisionViewSize, precisionPos);
            }

            vel = speed * x;

            cameraParent.Rotate(0, 0, vel);
            sling.Rotate(0, 0, vel);
        }

        private void EditorUpdate()
        {
            if (Input.GetMouseButton(0) && IsRotatableFromPointer(false))
            {
                Vector2 pos = camera.ScreenToViewportPoint(Input.mousePosition);
                Rotate(pos.x - offset, pos.y);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                SetCameraFocus(viewSize, originalPos);
            }
        }

        private void MobileUpdate()
        {
            //TODO: Get zoom working with touch input
            if (Input.touchCount > 0)
            {
                touch = Input.touches[0];
                Vector2 pos = camera.ScreenToViewportPoint(touch.position);

                if (IsRotatableFromPointer(true)) { Rotate(pos.x - offset, pos.y); }
            }
        }

        //TODO: Change to get rid of isMobile parameter
        private bool IsRotatableFromPointer(bool isMobile)
        {
            if (eventSystem)
            {
                if (isMobile) { return !eventSystem.IsPointerOverGameObject(touch.fingerId); }
                return !eventSystem.IsPointerOverGameObject();
            }
            return true;
        }
    }
}
