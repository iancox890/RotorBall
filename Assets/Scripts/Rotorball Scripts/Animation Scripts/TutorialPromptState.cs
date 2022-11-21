using UnityEngine;

namespace PsychedelicGames.RotorBall.Animations
{
    /// <summary>
    /// Controls the state for a tutorial prompt.
    /// </summary>
    public  class TutorialPromptState : StateMachineBehaviour
    {
        [SerializeField] private float time;

        private float currentTime;
        private int id = Animator.StringToHash("ExitLoop");

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => currentTime = 0;

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= time || Input.GetMouseButtonDown(0)) { animator.SetTrigger(id); }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) => animator.ResetTrigger(id);
    }
}
