using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PsychedelicGames.RotorBall.UI
{
    using LevelManagement;
    using Files;

    public class IfTutorialExists : MonoBehaviour
    {
        [SerializeField] private TutorialData tutorials;
        [SerializeField] private bool boostTutorials;
        [SerializeField] private bool ifTutorialCompleted;
        [SerializeField] private bool ifNoTutorialForThisLevel;
        [SerializeField] private UnityEvent functions;
        
        private void Awake()
        {
            tutorials = tutorials??Resources.Load<TutorialData>("Data/TutorialData");
            LevelData level = LevelData.Current;

            bool exists = boostTutorials ? tutorials.BoostTutorialsRequired() : tutorials.HasTutorial(level);
            bool completed = boostTutorials ? tutorials.BoostTutorialsRequired() : level.File.IsCompleted;

            bool existence  = exists ^ ifNoTutorialForThisLevel;
            bool completion = !(completed ^ ifTutorialCompleted) || !exists;
            if (existence && completion)
            {
                functions.Invoke();
            }
        }
    }
}