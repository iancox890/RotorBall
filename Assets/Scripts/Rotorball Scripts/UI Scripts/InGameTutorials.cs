using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    using LevelManagement;
    using Animations;
    using Boosts;
    public class InGameTutorials : MonoBehaviour
    {
        [SerializeField] private GameObject gameTutorials;
        [SerializeField] private GameObject boostTutorials;
        [SerializeField] private GameObject tutorialButton;
        [SerializeField] private TutorialData data;

        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();

            GetComponent<CanvasGroup>().blocksRaycasts = false;

            tutorialButton.Deactivate();
            boostTutorials.Activate();
            gameTutorials.Activate();
        }

        private void Start()
        {
            if (data.HasTutorial(LevelData.Current))
            {
                tutorialButton.Activate();
                if (!LevelData.Current.File.IsCompleted)
                {
                    ShowGameTutorials();
                }
                else if (data.BoostTutorialsRequired())
                {
                    ShowBoostTutorials();
                }
                else
                {
                    gameObject.Deactivate();
                }
            }
            else if (data.BoostTutorialsRequired())
            {
                ShowBoostTutorials();
            }
            else
            {
                gameObject.Deactivate();
            }
        }

        public void ShowGameTutorials()
        {
            //print("Show game tutorials");
            animator.SetTrigger("Tutorial Enter");
            gameTutorials.Activate();
            boostTutorials.Deactivate();
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        private void ShowBoostTutorials()
        {
            //print("Show boost tutorials");
            boostTutorials.Activate();
            boostTutorials.GetComponentInChildren<BoostInfoPanelAnimation>().SetBoosts(data.RequiredBoostTutorials());
            animator.SetTrigger("Tutorial Enter");
            gameTutorials.Deactivate();
            GetComponent<CanvasGroup>().blocksRaycasts = true;
        }

        public void HideGameTutorials()
        {
            //print("Hide game tutorials");
            //animator.SetTrigger("Tutorial Exit");
            if (data.BoostTutorialsRequired())
            {
                animator.SetTrigger("Tutorial Exit");
                // ShowBoostTutorials();
                StartCoroutine("WaitForGameTutorialsToClose");
            }
            else 
            {
                animator.SetTrigger("Tutorial Exit");
                boostTutorials.Deactivate();
                GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }

        public void HideBoostTutorials()
        {
            //print("Hide boost tutorials");
            animator.SetTrigger("Tutorial Exit");
            gameTutorials.Deactivate();
            GetComponent<CanvasGroup>().blocksRaycasts = false;

            foreach (Boost boost in data.RequiredBoostTutorials())
            {
                boost.CompleteTutorial();
            }
        }

        private IEnumerator WaitForGameTutorialsToClose() {
        {
            // yield return new WaitUntil(()=>{ print(animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Idle"));print(animator.GetCurrentAnimatorStateInfo(1).IsName("Base Layer.Idle"));print(animator.GetCurrentAnimatorStateInfo(-1).IsName("Base Layer.Idle"));return animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Idle"); });
            yield return new WaitUntil( ()=>animator.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Idle") );
            ShowBoostTutorials();
            yield break;
        }
        }
    }
}

