using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    using Colours;
    using Files;

    /// <summary>
    /// Displays the current colour scheme in the store.
    /// </summary>
    public class CurrentColourSchemeDisplay : MonoBehaviour
    {
        [SerializeField] private ColourScheme[] schemes;
        [SerializeField] private ColourScheme defaultScheme;

        //private void OnEnable() => defaultScheme.Apply();
        private void OnEnable()
        {
            defaultScheme.Apply();
        }

        private void OnDisable()
        {
            int length = schemes.Length;
            string currentlyActive = PlayerFile.GetFile().CurrentItems[(int)PlayerFile.Items.ColourScheme];

            for (int i = 0; i < length; i++)
            {
                ColourScheme scheme = schemes[i];
                if (scheme.name.Equals(currentlyActive))
                {
                    scheme.Apply();
                    break;
                }
            }
        }
    }
}
