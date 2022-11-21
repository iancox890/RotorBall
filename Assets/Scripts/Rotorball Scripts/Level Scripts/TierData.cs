using UnityEngine;

namespace PsychedelicGames.RotorBall.LevelManagement
{
    public enum LevelNumber
    {
        One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten,
        Eleven, Twelve, Thirteen, Fourteen, Fifteen, Sixteen, Seventeen, Eighteen, Nineteen, Twenty,
        TwentyOne, TwentyTwo, TwentyThree, TwentyFour, TwentyFive, TwentySix, TwentySeven, TwentyEight, TwentyNine, Thirty
    }

    /// <summary>
    /// Holds data related to a given tier.
    /// </summary>
    [CreateAssetMenu(fileName = "Tier Data", menuName = "Psychedelic Games/Levels/Tier Data")]
    public class TierData : ScriptableObject
    {
        [SerializeField] public int Number;
        [SerializeField] private LevelData[] data;
        [SerializeField] private bool lockLevels = true;
        [SerializeField] private int bitIndex;

        private int totalObjectivesCompleted = -1;
        private int totalLevelsCompleted = -1;

        public static TierData Current { get; set; }


        // TODO if PORT -ing to PC then this should be a file as player prefs saves to the registry (for whatever reason)
        public bool IsLocked {
            get
            {
                int tierUnlocks = PlayerPrefs.GetInt("Tiers");
                return (tierUnlocks & bitIndex) == 0;
            }
            set
            {
                int tierUnlocks = PlayerPrefs.GetInt("Tiers");
                tierUnlocks = (!value ? tierUnlocks | bitIndex : tierUnlocks & (~bitIndex));
                PlayerPrefs.SetInt("Tiers",tierUnlocks);
            }
        }


        public LevelData[] Data { get => data; }

        public int TotalObjectivesCompleted
        {
            get
            {
                totalObjectivesCompleted = 0;
                int length = data.Length;

                for (int i = 0; i < length; i++)
                {
                    bool[] objectivesCompleted = data[i].File.ObjectivesCompleted;
                    for (int j = 0; j < LevelData.ObjectiveCount; j++)
                    {
                        if (objectivesCompleted[j]) { totalObjectivesCompleted++; }
                    }
                }
                return totalObjectivesCompleted;
            }
        }

        public int TotalLevelsCompleted
        {
            get
            {
                totalLevelsCompleted = 0;
                int length = data.Length;

                for (int i = 0; i < length; i++)
                {
                    if (data[i].File.IsCompleted) { totalLevelsCompleted++; }
                }
                return totalLevelsCompleted;
            }
        }

        public LevelData GetData(int index)
        {
            if (index < data.Length)
            {
                return data[index];
            }

            Debug.Log("No data found at index: " + index);

            return null;
        }

        public void UpdateLevelStates()
        {
            data[0].IsLocked = false;
            for (int i = 1; i < data.Length; i++)
            {
                data[i].IsLocked = !data[i - 1].File.IsCompleted && lockLevels;
            }
        }

        public new void SetDirty()
        {
            totalObjectivesCompleted = -1;
            totalLevelsCompleted = -1;
        }

        public void ResetData()
        {
            int length = data.Length;
            for (int i = 0; i < length; i++)
            {
                data[i].ResetData();
            }
        }
    }
}
