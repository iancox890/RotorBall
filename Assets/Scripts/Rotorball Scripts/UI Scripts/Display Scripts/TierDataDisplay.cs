using UnityEngine;
using UnityEngine.UI;
using PsychedelicGames.RotorBall.LevelManagement;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Sets text to a particular piece of tier data.
    /// </summary>
    public class TierDataDisplay : MonoBehaviour
    {
        [SerializeField] private TierData data;
        [Space]
        [SerializeField] private Text completedLevels;
        [SerializeField] private Text completedObjectives;
        [Space]
        [SerializeField] private GameObject numberObj;
        [SerializeField] private GameObject lockObj;
        [SerializeField] private Button tierButtonObj;
        [SerializeField] private GameObject unlockButtonObj;
        [Space]
        [SerializeField] private GameObject statisticsObj;

        private bool ready;

        private void Awake()
        {
            ready = false;
        }

        private void OnEnable()
        {
            if (ready)
            {
                CheckIfLocked();
            }
        }

        private void Start()
        {
            ready = true;
            CheckIfLocked();
        }

        public void CheckIfLocked()
        {
            //print(gameObject.name + " is " + (data.IsLocked ? "locked" : "unlocked"));
            if (data.IsLocked)
            {
                numberObj.Deactivate();
                statisticsObj.Deactivate();

                lockObj.Activate();
                unlockButtonObj.Activate();

                tierButtonObj.interactable = false;

                //GetComponent<CanvasGroup>().interactable = false;
            }
            else
            {
                completedLevels.text = data.TotalLevelsCompleted.ToString();
                completedObjectives.text = data.TotalObjectivesCompleted.ToString();

                numberObj.Activate();
                statisticsObj.Activate();

                lockObj.Deactivate();
                unlockButtonObj.Deactivate();

                tierButtonObj.interactable = true;

                //GetComponent<CanvasGroup>().interactable = true;
            }
        }
    }
}
