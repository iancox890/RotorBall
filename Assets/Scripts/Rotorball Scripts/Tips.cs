using UnityEngine;

namespace PsychedelicGames.RotorBall
{
    /// <summary>
    /// Holds and generates a tip when prompted.
    /// </summary>
    [CreateAssetMenu(fileName = "Tips.asset", menuName = "Psychedelic Games/Tips")]
    public class Tips : ScriptableObject
    {
        [SerializeField] private Tip[] tips;

        public Tip GetTip() => tips[Random.Range(0, tips.Length)];
    }

    [System.Serializable]
    public struct Tip
    {
        [SerializeField] private string header;
        [SerializeField] private string description;

        public string Header { get => header; }
        public string Description { get => description; }
    }
}
