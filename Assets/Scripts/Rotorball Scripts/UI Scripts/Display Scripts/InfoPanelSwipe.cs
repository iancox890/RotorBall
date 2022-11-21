using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PsychedelicGames.RotorBall.UI
{
    using Animations;
    public class InfoPanelSwipe : MonoBehaviour
    {
        [SerializeField] private InfoPanelAnimatorGeneric infoPanels;
        [SerializeField] private float dragDistance;
        private Vector2 start;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        start = touch.position;
                        break;
                    case TouchPhase.Moved:
                        // feedback
                        break;
                    case TouchPhase.Ended:
                        if (Vector2.Distance(touch.position,start) > dragDistance)
                        {

                        }
                        break;
                }
            }
        }
    }
}
