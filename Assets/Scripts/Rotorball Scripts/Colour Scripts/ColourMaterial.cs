using UnityEngine;

namespace PsychedelicGames.RotorBall.Colours
{
    /// <summary>
    /// Represents a colour and object material.
    /// </summary>
    [System.Serializable]
    public struct ColourMaterial
    {
        [SerializeField] private Material colourMat;
        [SerializeField] private Material objectMat;

        public Material ColourMat { get => colourMat; }
        public Material ObjectMat { get => objectMat; }

        public void AssignColour()
        {
            if (objectMat && colourMat)
            {
                objectMat.SetColor("_Color", colourMat.color);
                objectMat.EnableKeyword("");
            }
            else
            {
                Debug.Log("Material(s) not assigned to!");
            }
        }
    }
}
