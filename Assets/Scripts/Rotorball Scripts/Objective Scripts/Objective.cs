using UnityEngine;
using PsychedelicGames.RotorBall.LevelManagement;

namespace PsychedelicGames.RotorBall.Objectives
{
    /// <summary>
    /// Base class for a given objective.
    /// </summary>
    public abstract class Objective : ScriptableObject
    {
        [SerializeField] protected StringVariable description;

        public virtual string Description { get => description.Value; }
        public abstract bool UpdateStatus(LevelReport report);
    }
}
