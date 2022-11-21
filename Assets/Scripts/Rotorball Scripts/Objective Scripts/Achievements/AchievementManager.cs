using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;

namespace PsychedelicGames.RotorBall.Objectives.Achievments
{
    using Files;
    using LevelManagement;
    // shrink boosts
    // shifter bricks
    // rotorpoints
    // supercharge boost
    public class AchievementManager
    {
        private const bool DEBUG = false;
        readonly private static System.Type[] statTypes = { typeof(int), typeof(float), typeof(double), typeof(uint) };
        private static AchievementData _achievementData;
        public static AchievementData AchievementData
        {
            get
            {
                if (_achievementData == null)
                {
                    _achievementData = Resources.Load<AchievementData>("Data/AchievementData");
                }
                return _achievementData;
            }
        }

        public static void CheckAchievements(StatisticsFile stats, bool printStats)
        {
            PropertyInfo[] props = stats.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (statTypes.Any(t => t == prop.PropertyType))
                {
                    double value;
                    if (double.TryParse(prop.GetValue(stats).ToString(), out value))
                    {
                        SubmitStatistic("stats." + prop.Name, value, printStats);
                    }
                }
            }
        }
        public static void CheckAchievements(TierData[] tiers, bool printStats)
        {
            foreach (TierData tier in tiers)
            {
                CheckAchievements(tier, printStats);
            }
        }
        public static void CheckAchievements(TierData tier, bool printStats)
        {
            PropertyInfo[] props = tier.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (statTypes.Any(t => t == prop.PropertyType))
                {
                    double value;
                    if (double.TryParse(prop.GetValue(tier).ToString(), out value))
                    {
                        SubmitStatistic($"tier-{tier.Number}.{prop.Name}", value, printStats);
                    }
                }
            }
        }

        public static void SubmitStatistic(string name, int stat)
        {
            SubmitStatistic(name, (double)stat, false);
        }
        public static void SubmitStatistic(string name, float stat)
        {
            SubmitStatistic(name, (double)stat, false);
        }
        private static void SubmitStatistic(string statName, double statValue, bool printStats)
        {
            if (printStats) { Debug.Log($"Checking stat \"{statName}\""); }
            foreach (Achievement achievement in AchievementData.GetAllByStat(statName))
            {
                int index = AchievementData.IndexOf(achievement);
                if (!IsAchieved(index))
                {
                    if (statValue >= achievement.threshold)
                    {
                        Debug.Log($"Achievement: \"{achievement.name}\"\nStat: \"{statName}\"\nUnlocked! Progress: {statValue}/{achievement.threshold}");
                        Achieve(index);
                    }
                    else
                    {
                        Debug.Log($"Achievement: \"{achievement.name}\", Stat: \"{statName}\"\nLocked ): Progress: {statValue}/{achievement.threshold}\n");
                    }
                }
                else { Debug.Log("Already achieved!"); }
            }
        }
        public static bool IsAchieved(Achievement achievement) => IsAchieved(AchievementData.IndexOf(achievement));
        private static bool IsAchieved(int index)
        {
            if (index >= 0)
            {
                int achievementSet = index / 32;
                int a = PlayerPrefs.GetInt("ACHIEVEMENTS" + achievementSet.ToString());
                if (DEBUG) { Debug.Log($"Achievements: {System.Convert.ToString(a, 2)}"); }
                return (a & (int)Mathf.Pow(2f, (float)index)) > 0;
            }
            else { return false; }
        }
        public static void Achieve(Achievement achievement) => Achieve(AchievementData.IndexOf(achievement));
        public static void Achieve(string name) => Achieve(AchievementData.IndexOf(name));
        private static void Achieve(int index)
        {
            int achievementSet = index / 32;
            int a = PlayerPrefs.GetInt("ACHIEVEMENTS" + achievementSet.ToString());
            if (DEBUG) { Debug.Log($"Adding achievement to set #{achievementSet}\nAchievements (Before): {System.Convert.ToString(a, 2)}"); }
            a |= (int)Mathf.Pow(2f, (float)index);
            if (DEBUG) { Debug.Log($"Achievements (After): {System.Convert.ToString(a, 2)}"); }
            PlayerPrefs.SetInt("ACHIEVEMENTS" + achievementSet.ToString(), a);
        }

        public static void PrintAll() {
            foreach (Achievement a in AchievementData.GetAll()) {
                string unlocked = IsAchieved(a) ? "unlocked" : "locked";
                Debug.Log($"{a.name} is {unlocked}");
            }
        }
    }
}