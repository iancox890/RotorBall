using UnityEngine;
using UnityEngine.UI;
using PsychedelicGames.RotorBall.LevelManagement;

namespace PsychedelicGames.RotorBall.UI
{
    /// <summary>
    /// Sets text to a particluar level info string.
    /// </summary>
    public class LevelDataText : MonoBehaviour
    {
        [SerializeField] private InfoCode code;

        private enum InfoCode
        {
            ObjectiveOneDescription = 0, ObjectiveTwoDescription = 1, ObjectiveThreeDescription = 2,//Match enum value with indices of the objectives array. 
            LevelNumber, LevelDescription, RecordTime, RecordScore,
            BrickCount, BallCount, ExtraBallCount, BallLifeTime, BallSpeed, BallSize
        }

        private Text infoText;
        private LevelData data;

        private void Awake()
        {
            infoText = GetComponent<Text>();
            data = LevelData.Current;
        }

        private void OnEnable()
        {
            if (data)
            {
                switch (code)
                {
                    case InfoCode.LevelNumber:
                        infoText.text = data.Number;
                        break;
                    case InfoCode.LevelDescription:
                        infoText.text = data.Description;
                        break;
                    case InfoCode.RecordTime:
                        infoText.text = data.File.RecordTime.ToFormattedTime();
                        break;
                    case InfoCode.RecordScore:
                        int score = data.File.RecordScore;
                        infoText.text = score.FormatValue();
                        break;
                    case InfoCode.BrickCount:
                        infoText.text = data.Modifiers.BrickCount.FormatValue();
                        break;
                    case InfoCode.BallCount:
                        infoText.text = data.Modifiers.BallCount.FormatValue();
                        break;
                    case InfoCode.ExtraBallCount:
                        int count = data.Modifiers.ExtraBallCount;
                        if (count > 1)
                        {
                            infoText.text = count.FormatValue() + "x FREE BALLS!";
                        }
                        else
                        {
                            infoText.text = count.FormatValue() + "x FREE BALL!";
                        }
                        break;
                    case InfoCode.BallLifeTime:
                        infoText.text = data.Modifiers.BallLifeTime.FormatValue();
                        break;
                    case InfoCode.BallSpeed:
                        infoText.text = data.Modifiers.BallSpeed.ToString("0.00") + "x";
                        break;
                    case InfoCode.BallSize:
                        infoText.text = data.Modifiers.BallSize.ToString("0.00") + "x";
                        break;
                    default:
                        infoText.text = data.Objectives[(int)code].Description;
                        break;
                }
            }
            else { Debug.Log("No LevelData found!"); }
        }
    }
}
