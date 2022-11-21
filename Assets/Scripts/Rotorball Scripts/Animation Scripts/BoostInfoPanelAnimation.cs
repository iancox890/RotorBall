using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace PsychedelicGames.RotorBall.Animations
{
    using Boosts;
    /// <summary>
    /// Handles the boost tutorial information panels.
    /// </summary>
    public class BoostInfoPanelAnimation : InfoPanelNumberedAnimation
    {
        public Animator[] sequence2 = null;

        public void SetBoosts(Boost[] boosts)
        {
            if (boosts.Length == 0)
            {
                // print("Empty boosts");
                gameObject.Deactivate();
            } else
            {
                // print("Non empty boosts");
                index = 0;
                sequence2 = sequence.Where((s) => boosts.Contains(s.GetComponent<BoostInfoPanel>().GetBoost())).ToArray();
                foreach (Animator a in sequence.Except(sequence2))
                {
                    a.gameObject.Deactivate();
                }
                gameObject.Activate();
                UpdatePageNumber();
                GetCurrent().Play(slideInRight,-1,1f);
            }
        }

        private void Awake()
        {
            index = 0;
            sequence2 = sequence2 ?? sequence;
            if (GetLength() > 0)
            {
                UpdatePageNumber();
                GetCurrent().Play(slideInRight,-1,1f);
            }
            else
            {
                //gameObject.Deactivate();
            }
        }

        protected override int GetLength()
        {
            return sequence2.Length;
        }
        protected override Animator GetCurrent()
        {
            return sequence2[index];
        }
        protected override Animator GetNext()
        {
            //if (GetLength() == 0) { return null; }
            return sequence2[index + 1 > GetLength() - 1 ? 0 : index + 1];
        }
        protected override Animator GetPrev()
        {
            //print($"{name}:{GetLength()}");
            //if (GetLength() == 0) { return null; }
            return sequence2[index - 1 < 0 ? GetLength() - 1 : index - 1];
        }

        override protected void UpdatePageNumber()
        {
            numberText.text = (index + 1).ToString() + " / " + GetLength().ToString();
        }
    }
}
