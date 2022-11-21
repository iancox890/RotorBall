using UnityEngine;

namespace PsychedelicGames.RotorBall.Gameplay
{
    /// <summary>
    /// Moves a given object between at least 2 points.
    /// </summary>
    public class ObjMover : MonoBehaviour
    {
        [SerializeField] private Vector2[] points = new Vector2[2];
        [SerializeField] private float speed;
        [SerializeField] private new Rigidbody2D rigidbody;

        private DestructibleBrick destructible;

        private Vector2 currentPoint;
        private Vector2 desiredPoint;

        private float percent;

        private int index;
        private int length;

        private void Awake()
        {
            currentPoint = points[index];
            desiredPoint = points[++index];

            length = points.Length;
            destructible = rigidbody.GetComponentInChildren<DestructibleBrick>();
        }

        private void OnEnable()
        {
            if (destructible)
            {
                destructible.OnBrickDestroyed += Disable;
            }
        }

        private void OnDisable()
        {
            if (destructible)
            {
                destructible.OnBrickDestroyed -= Disable;
            }
        }

        private void FixedUpdate()
        {
            if (percent != 1)
            {
                rigidbody.MovePosition(transform.TransformPoint(Vector2.Lerp(currentPoint, desiredPoint, percent)));
                percent = percent > 1 ? 1 : percent + (speed * Time.deltaTime);
            }
            else
            {
                currentPoint = desiredPoint;
                percent = 0;

                index = index < (length - 1) ? index + 1 : 0;
                desiredPoint = points[index];
            }
        }

        private void Disable() => enabled = false;
    }
}
