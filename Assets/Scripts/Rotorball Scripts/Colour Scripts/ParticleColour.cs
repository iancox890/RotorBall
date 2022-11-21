using UnityEngine;

namespace PsychedelicGames.RotorBall.Particles
{
    /// <summary>
    /// Sets the colour of a particle system based off a given material.
    /// </summary>
    public class ParticleColour : MonoBehaviour
    {
        [SerializeField] private Material material;

        private void Start()
        {
            var main = GetComponent<ParticleSystem>().main;
            main.startColor = material.color;
        }
    }
}
