using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PsychedelicGames.RotorBall.Objectives.Achievments
{
    [CreateAssetMenu(fileName = "AchievementData", menuName = "Psychedelic Games/Data/Achievement Data Asset")]
    public class AchievementData : ScriptableObject
    {
        [SerializeField] private Achievement[] achievements;

        public bool Has(string name) => achievements.Any(a => a.name == name);
        // public bool Get(string name,out Achievement achievement) {
        //     if (Has(name)) {
        //         achievement = achievements.FirstOrDefault(a => a.name == name);
        //         return true;
        //     }
        //     achievement = null;
        //     return false;
        // }
        public Achievement[] GetAll() => achievements;
        public int[] GetIndicesByStat(string statName) => (from a in achievements.Where(a => a.statName == statName) select IndexOf(a.name)).ToArray();
        public Achievement[] GetAllByStat(string statName) => achievements.Where(a => a.statName == statName).ToArray();
        public Achievement Get(string name) => achievements.FirstOrDefault(a => a.name == name);

        public Achievement Get(int index) { return achievements[index]; }

        public int IndexOf(Achievement achievement) {
            int i=0;
            foreach (Achievement a in achievements) {
                if (achievement == a) {
                    return i;
                } i++;
            }
            return -1;
        }
        public int IndexOf(string search)
        {
            string search2 = search.ToLower();
            int index = 0;
            foreach (Achievement a in achievements)
            {
                if (search2 == a.name.ToLower())
                {
                    return index;
                }
                index++;
            }
            return -1;
        }
    }
}