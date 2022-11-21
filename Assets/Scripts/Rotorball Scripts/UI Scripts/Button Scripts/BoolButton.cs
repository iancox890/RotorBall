using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PsychedelicGames.RotorBall.UI
{
    public class BoolButton : UIButton
    {
        [SerializeField] public SVGImage tickImage;
        [SerializeField] private bool value;
        [SerializeField] private string key;
        [SerializeField] private GameObject other;

        protected override void Init()
        {
            base.Init();
            if (PlayerPrefs.GetInt(key) == (value?1:0))
            {
                //button.interactable = false;
                tickImage.enabled = true;
            }
            else
            {
                //button.interactable = true;
                tickImage.enabled = false;
            }
        }

        protected override void OnClicked()
        {
            PlayerPrefs.SetInt(key, value?1:0);
            PlayerPrefs.Save();
            //other.GetComponent<UnityEngine.UI.Button>().interactable = true;
            other.GetComponent<BoolButton>().tickImage.enabled = false;
            //button.interactable = false;
            tickImage.enabled = true;
        }
    }
}