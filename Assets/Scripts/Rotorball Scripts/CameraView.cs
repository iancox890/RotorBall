using UnityEngine;
using PsychedelicGames.RotorBall.Gameplay;

namespace PsychedelicGames.RotorBall
{
    /// <summary>
    /// Sets the camera size appropriately depending on the screen size.
    /// </summary>
    public class CameraView : MonoBehaviour
    {
        [SerializeField] private float farSize;
        [SerializeField] private float mediumSize;
        [SerializeField] private float closeSize;
        [SerializeField] private bool overridePreference;

        public void SetView(int index)
        {
            Camera camera = GetComponent<Camera>();
            float size = 0;

            switch (index)
            {
                case 0:
                    size = closeSize;
                    break;
                case 1:
                    size = mediumSize;
                    break;
                case 2:
                    size = farSize;
                    break;
            }

            camera.orthographicSize = size / camera.aspect;
        }

#if UNITY_EDITOR
        private void Awake()
        {
            if (overridePreference == false)
            {
                SetView(PlayerPrefs.GetInt("View Setting", 1));
            }
        }
#else
        private void Awake()
        {
            SetView(PlayerPrefs.GetInt("View Setting", 1));
        }
#endif
    }
}
