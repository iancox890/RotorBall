using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    using LevelManagement;

    /// <summary>
    /// Sets/updates which tiers are locked.
    /// </summary>
    public class TierStatesDisplay : MonoBehaviour
    {
        [SerializeField] private TierData[] data;
        [SerializeField] private bool lockTiers = false;

        private void Awake()
        {
            if (lockTiers)
            {
                int length = data.Length;
                for (int i = 0; i < length; i++)
                {
                    if (i == 0)
                    {
                        data[i].IsLocked = false;
                    }
                    else if (data[i - 1].TotalLevelsCompleted >= 30)
                    {
                        data[i].IsLocked = false;
                    }
                    //print("Tier " + (i + 1).ToString() + " is " + (data[i].IsLocked ? "locked" : "unlocked"));
                }
            }
        }
    }
}
