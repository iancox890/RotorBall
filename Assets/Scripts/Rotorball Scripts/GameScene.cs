using UnityEngine;

namespace PsychedelicGames.RotorBall
{
    /// <summary>
    /// Represents a scene with a given path.
    /// </summary>
    [CreateAssetMenu(fileName = "Scene", menuName = "Psychedelic Games/Scene")]
    public  class GameScene : ScriptableObject
    {
        [SerializeField] private string scenePath;
        [SerializeField] private string sceneName;

        public string ScenePath { get => scenePath; }
        public string SceneName { get => sceneName; }
    }
}
