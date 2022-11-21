using UnityEngine;

namespace PsychedelicGames
{
    /// <summary>
    /// Acts as a string variable.
    /// </summary>
    [CreateAssetMenu(fileName = "StringVariable.asset", menuName = "Psychedelic Games/Variables/String")]
    public  class StringVariable : ScriptableObject
    {
        [SerializeField] private string value;
        public string Value { get => value; }
    }
}
