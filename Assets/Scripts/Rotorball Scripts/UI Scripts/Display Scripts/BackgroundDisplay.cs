using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Controls the background display scrolling.
    /// </summary>
    public class BackgroundDisplay : MonoBehaviour
    {
        [SerializeField] private Image[] image;
        [Space]
        [SerializeField] private float xSpeed;
        [SerializeField] private float ySpeed;

        private Vector2 offset;
        private Vector2 zero = Vector2.zero;
        private Material[] mats;
        private Material currentMat;
        private int length;

        private void Start()
        {
            length = image.Length;
            mats = new Material[length];
            for (int i = 0; i < length; i++)
            {
                mats[i] = image[i].material;
            }
        }

        private void LateUpdate()
        {
            float delta = Time.deltaTime;

            offset.x += xSpeed * delta;
            offset.y += ySpeed * delta;

            for (int i = 0; i < length; i++)
            {
                mats[i].mainTextureOffset = offset;
            }
        }

        private void OnDestroy()
        {

            for (int i = 0; i < length; i++)
            {
                mats[i].mainTextureOffset = zero;
            }
        }

        private void OnFadeOut() => gameObject.Deactivate();
    }
}
