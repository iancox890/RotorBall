using UnityEngine;
using UnityEngine.UI;
using PsychedelicGames.RotorBall.LevelManagement;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Displays objective info based on the current level data.
    /// </summary>
    public class ObjectiveDisplay : MonoBehaviour
    {
        [SerializeField] private Objective objectiveNumber;
        [SerializeField] private bool useCurrentData = true;
        [SerializeField] private LevelNumber levelNumber;
        [SerializeField] private Image[] images;
        [SerializeField] private Sprite completed;
        [SerializeField] private Sprite incompleted;

        private LevelData data;
        private bool[] objectives;
        private int length;

        public enum Objective { One = 0, Two = 1, Three = 2, All }

        private void Awake()
        {
            if (useCurrentData)
            {
                data = LevelData.Current;
            }
            else
            {
                data = TierData.Current.GetData((int)levelNumber);
            }

            if (data)
            {
                objectives = data.File.ObjectivesCompleted;
                length = images.Length;

                bool isCompleted;
                if ((objectiveNumber != Objective.All))
                {
                    isCompleted = objectives[(int)objectiveNumber];
                    for (int i = 0; i < length; i++)
                    {
                        if (isCompleted)
                        {
                            images[i].sprite = completed;
                        }
                        else
                        {
                            images[i].sprite = incompleted;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < length; i++)
                    {
                        isCompleted = objectives[i];
                        if (isCompleted)
                        {
                            images[i].sprite = completed;
                        }
                        else
                        {
                            images[i].sprite = incompleted;
                        }
                    }
                }
            }
            else
            {
                Debug.Log("No LevelData found!");
            }
        }
    }
}
