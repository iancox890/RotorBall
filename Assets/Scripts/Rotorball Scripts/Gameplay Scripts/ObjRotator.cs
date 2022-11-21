using UnityEngine;

namespace PsychedelicGames.RotorBall.Gameplay
{
    /// <summary>
    /// Rotates an object with a given speed and direction.
    /// </summary>
    public class ObjRotator : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Direction direction;

        private new Transform transform;
        private float angle;

        public enum Direction
        {
            Clockwise = -1,
            Counterclockwise = 1
        }

        private void Start()
        {
            transform = GetComponent<Transform>();
            transform = transform ?? GetComponent<RectTransform>();
            speed *= (int)direction;
        }

        private void Update() => transform.Rotate(0, 0, speed * Time.deltaTime);
    }
}