using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace PsychedelicGames.RotorBall.Animations
{
    /// <summary>
    /// Handles the level details information panels.
    /// </summary>
    public abstract class InfoPanelAnimatorGeneric : MonoBehaviour
    {
        [SerializeField] protected GameObject background;
        [SerializeField] protected Animator[] sequence;

        protected int slideOutLeft = Animator.StringToHash("Slide Out Left");
        protected int slideInLeft = Animator.StringToHash("Slide In Left");
        protected int slideOutRight = Animator.StringToHash("Slide Out Right");
        protected int slideInRight = Animator.StringToHash("Slide In Right");
        protected int index;

        [SerializeField] [Range(0f,1f)] protected float dragDistance = 0.2f;
        [SerializeField] [Range(0f, 1f)] protected float deadzone = 0.05f;
        protected Vector2 start;
        protected GraphicRaycaster graycaster;
        protected bool dragging;

        private void Update()
        {
            if (GetLength() > 1)
            {
                #if UNITY_EDITOR
                    bool debug_enabled = false;
                    // panel disappears in deadzone
                    if (Input.GetMouseButton(0))// && !EventSystem.current.IsPointerOverGameObject())
                    {
                        Vector2 mouse_pos = Input.mousePosition;
                        bool dragAllowed = false;

                        PointerEventData ped = new PointerEventData(EventSystem.current);
                        ped.position = mouse_pos;
                        List<RaycastResult> results = new List<RaycastResult>();
                        graycaster.Raycast(ped, results);

                        foreach (RaycastResult res in results)
                        {
                            if (res.gameObject.Equals(background))
                            {
                                dragAllowed = true;
                            }
                        }

                        if (dragAllowed)
                        {
                            if (Input.GetMouseButtonDown(0))
                            {
                                dragging = true;
                                start = mouse_pos;
                            }
                            else
                            {
                                start.y = mouse_pos.y;

                                float dist = Vector2.Distance(Input.mousePosition, start);
                                float pcnt = dist / Screen.width;
                                if (pcnt > deadzone)
                                {
                                    if (start.x > Input.mousePosition.x)
                                    {
                                        if (debug_enabled)
                                        { print($"left (next): {(pcnt - deadzone) / dragDistance}"); }
                                        GetPrev().Play(slideOutLeft, -1, 1f);
                                        GetCurrent().Play(slideOutLeft, -1, (pcnt - deadzone) / dragDistance);
                                        GetNext().Play(slideInRight, -1, (pcnt - deadzone) / dragDistance);
                                    }
                                    else if (start.x < Input.mousePosition.x)
                                    {
                                        if (debug_enabled)
                                        { print($"right (prev): {(pcnt - deadzone) / dragDistance}"); }
                                        GetNext().Play(slideOutRight, -1, 1f);
                                        GetCurrent().Play(slideOutRight, -1, (pcnt - deadzone) / dragDistance);
                                        GetPrev().Play(slideInLeft, -1, (pcnt - deadzone) / dragDistance);
                                    }
                                }
                                else
                                {
                                    if (debug_enabled)
                                    { print("resetting all"); }
                                    GetPrev().Play(slideOutLeft, -1, 1f);
                                    GetCurrent().Play(slideInLeft, -1, 1f);
                                    GetNext().Play(slideOutRight, -1, 1f);
                                }
                            }
                        }
                    }
                    if (Input.GetMouseButtonUp(0) && dragging)
                    {
                        dragging = false;
                        start.y = Input.mousePosition.y;
                        float dist = Vector2.Distance(Input.mousePosition, start);
                        float pcnt = dist / Screen.width;
                        if (pcnt > deadzone)
                        {
                            if (start.x > Input.mousePosition.x)
                            {
                                if (debug_enabled)
                                { print("next"); }
                                Next();
                            }
                            else if (start.x < Input.mousePosition.x)
                            {
                                if (debug_enabled)
                                { print("prev"); }
                                Prev();
                            }
                        }
                        else
                        {
                            if (debug_enabled) { print("resetting all"); }
                            GetPrev().Play(slideOutLeft, -1, 1f);
                            GetCurrent().Play(slideInLeft, -1, 1f);
                            GetNext().Play(slideOutRight, -1, 1f);
                        }
                    }
                #elif UNITY_ANDROID || UNITY_IOS
                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);
                        float dist;
                        float pcnt;
                        switch (touch.phase)
                        {
                            case TouchPhase.Began:
                                start = touch.position;
                                break;
                            case TouchPhase.Moved:
                                start.y = touch.position.y;
                                dist = Vector2.Distance(touch.position, start);
                                pcnt = dist / Screen.width;
                                if (pcnt > deadzone)
                                {
                                    if (start.x > touch.position.x)
                                    {
                                        GetPrev().Play(slideOutLeft, -1, 1f);
                                        GetCurrent().Play(slideOutLeft, -1, (pcnt-deadzone) / dragDistance);
                                        GetNext().Play(slideInRight, -1, (pcnt - deadzone) / dragDistance);
                                    }
                                    else if (start.x < touch.position.x)
                                    {
                                        GetNext().Play(slideOutRight, -1, 1f);
                                        GetCurrent().Play(slideOutRight, -1, (pcnt - deadzone) / dragDistance);
                                        GetPrev().Play(slideInLeft, -1, (pcnt - deadzone) / dragDistance);
                                    }
                                }
                                else
                                {
                                    GetPrev().Play(slideOutLeft, -1, 1f);
                                    GetCurrent().Play(slideInLeft, -1, 1f);
                                    GetNext().Play(slideOutRight, -1, 1f);
                                }
                                break;
                            case TouchPhase.Ended:
                                start.y = touch.position.y;
                                dist = Vector2.Distance(touch.position, start);
                                pcnt = dist / Screen.width;
                                if (pcnt > deadzone)
                                {
                                    if (start.x > touch.position.x)
                                    {
                                        Next();
                                    }
                                    else if (start.x < touch.position.x)
                                    {
                                        Prev();
                                    }
                                }
                                else
                                {
                                    GetPrev().Play(slideOutLeft, -1, 1f);
                                    GetCurrent().Play(slideInLeft, -1, 1f);
                                    GetNext().Play(slideOutRight, -1, 1f);
                                }
                                break;
                        }
                    }
                #endif
            }
            else if (GetLength() == 1)
            {
                // print("Single panel genericinfopanelanimator");
                GetCurrent().Play(slideInLeft, -1, 1f);
            }
        }

        public virtual void NextPage()
        {
            print("NEXT");

            //Slide out the current panel
            GetCurrent().Play(slideOutLeft,-1,0f);

            //If we're at the end of the array, start at the beginning.
            //If not, progress forward.
            if ((index + 1) > (GetLength() - 1)) { index = 0; }
            else { index++; }

            //Slide in the next panel.
            GetCurrent().Play(slideInRight, -1, 0f);
        }

        public virtual void PreviousPage()
        {
            print("PREVIOUS");

            //Slide out the current panel
            GetCurrent().Play(slideOutRight, -1, 0f);

            //If we're at the beginning of the array, start at the end.
            //If not, progress backwards.
            if ((index - 1) < 0) { index = GetLength() - 1; }
            else { index--; }

            //Slide in the next panel.
            GetCurrent().Play(slideInLeft, -1, 0f);
        }

        public virtual void Next()
        {
            NextPage();
        }
        public virtual void Prev()
        {
            PreviousPage();
        }

        protected virtual int GetLength()
        {
            return sequence.Length;
        }

        protected virtual Animator GetCurrent()
        {
            return sequence[index];
        }
        protected virtual Animator GetNext()
        {
            return sequence[index + 1 > GetLength() - 1 ? 0 : index + 1];
        }
        protected virtual Animator GetPrev()
        {
            return sequence[index - 1 < 0 ? GetLength() - 1 : index - 1];
        }

        virtual protected void Start()
        {
            if (background == null)
            {
                throw new UnityException($"Info Panel Animator {this.gameObject.name} needs the background property set for swiping to work. The background GameObject needs an image and a canvas group so it can be correctly raycasted. Thanks! :)");
            }
            graycaster = FindObjectOfType<GraphicRaycaster>();
        }
    }
}
