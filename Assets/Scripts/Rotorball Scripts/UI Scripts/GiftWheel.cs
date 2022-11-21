using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace PsychedelicGames.RotorBall
{
    using Files;
    using UI;

    public class GiftWheel : MonoBehaviour
    {
        //[SerializeField] private GiftReset gift;
        //[Space]
        [SerializeField] UIButton backButton;
        [SerializeField] GameObject giftScreen;
        [SerializeField] GameObject animationScreen;
        [SerializeField] GameObject bonusButtons;
        [SerializeField] GameObject timeRemaining;
        [SerializeField] GameObject itemAwarded;
        [SerializeField] GameObject wheel;
        [SerializeField] GiftWheelSegment[] segments;
        [Space]
        [SerializeField] Boosts.Boost chainBoost;
        [SerializeField] Boosts.Boost enduranceBoost;
        [SerializeField] Boosts.Boost scoreBoost;
        [SerializeField] Boosts.Boost shrinkBoost;
        [SerializeField] Boosts.Boost enlargeBoost;
        [SerializeField] Boosts.Boost superBoost;
        [Space]
        [SerializeField] Animator animator;
        [Space]
        [SerializeField] Image[] giftIcons;
        [SerializeField] Text[] giftQuantities;
        [SerializeField] Text[] giftDescriptions;
        [SerializeField] string defaultDescription;
        [Space]
        [SerializeField] [Range(0.5f,25f)] private float maxSpeed;
        [SerializeField] [Range(0.5f,25f)] private float minSpeed;
        [SerializeField] [Range(0.15f,20f)] private float slowDown;
        [Space]
        [SerializeField] Material highlightedSeg;
        [SerializeField] Material highlightedItemImg;
        [SerializeField] Material highlightedItemQuantity;
        [SerializeField] Material unhighlightedSeg;
        [SerializeField] Material unhighlightedItemImg;
        [SerializeField] Material unhighlightedItemQuantity;

        public UnityEvent OnUnskippable;
        [System.NonSerialized]
        public bool IsSkippable;

        private bool  extraSpin;
        private bool  spinning;
        private float angle;
        private float speed;
        private float segmentAngle;
        private float halfSegment;
        private float slowMod;
        private int   hash;

        private GiftWheelSegment prevSeg;

        private void Awake()
        {
            spinning = false;
            speed = 0f;
            segmentAngle = 2f * Mathf.PI / segments.Length;
            halfSegment = segmentAngle / 2f;
            prevSeg = segments[0];
            //hash = Animator.StringToHash(spinCompleteAnimation);
            giftScreen.Activate();
            animationScreen.Deactivate();
        }

        private void OnEnable()
        {
            timeRemaining.Activate();
            itemAwarded.Deactivate();
            if (extraSpin)
            {
                extraSpin = false;
            } else
            {
                backButton.Interactable = true;//gameObject.Activate();
                bonusButtons.Activate();
            }
        }

        private void Start() {
            // TODO fix/remove
            // check that all angles work correctly (some weird crash was happening before)
            // for (int i=0; i<segments.Length; i++) {
            //     SetAngle(i * segmentAngle);
            // }
            SetAngle(Random.Range(0,segments.Length) * segmentAngle);
        }

        private void Update()
        {
            if (spinning)
            {
                speed -= Time.deltaTime * slowDown * slowMod;
                if (speed <= 0f)
                {
                    StopSpinning();
                } else
                {
                    if (IsSkippable && (speed / minSpeed) < 0.5f) {
                        IsSkippable = false;
                        OnUnskippable.Invoke();
                    }
                    
                    if (speed > minSpeed/8f)
                    {
                        slowMod = Mathf.Clamp(speed / maxSpeed, 0.01f, 1f);
                    }
                    angle += Time.deltaTime * speed;
                    SetAngle(angle + Time.deltaTime * speed);
                }
            }
        }

        private GiftWheelSegment GetSelectedSegment()
        {
            int index = segments.Length - (int)((angle + halfSegment) / segmentAngle);
            if (index >= 0 && index < segments.Length)
            {
                return segments[index];
            } else if (index == segments.Length)
            {
                return segments[0];
            } else
            {
                return null;
            }
        }

        private void SetAngle(float newAngle)
        {
            angle = newAngle;
            if (angle >= 2f * Mathf.PI)
            {
                angle -= 2f * Mathf.PI;
            }
            Vector3 rot = wheel.transform.eulerAngles;
            rot.z = Mathf.Rad2Deg * angle;
            wheel.transform.eulerAngles = rot;

            GiftWheelSegment seg = GetSelectedSegment();
            if (seg != prevSeg)
            {
                prevSeg.SetMaterials(unhighlightedSeg, unhighlightedItemImg, unhighlightedItemQuantity);
                seg.SetMaterials(highlightedSeg, highlightedItemImg, highlightedItemQuantity);

                // Set and bounce gift info
                foreach (Image icon in giftIcons)
                {
                    icon.sprite = seg.GetImage();
                    icon.GetComponent<Bounce>()?.DoBounce();
                }
                foreach (Text quantity in giftQuantities)
                {
                    quantity.text = seg.giftQuantity.ToString();
                    quantity.GetComponent<Bounce>()?.DoBounce();
                }
                foreach (Text description in giftDescriptions)
                {
                    // Set gift description
                    switch (seg.giftType)
                    {
                        case GiftType.Rotorpoints:
                            description.text = "ROTORPOINT" + (seg.giftQuantity != 1 ? "S" : "");
                            break;
                        case GiftType.ChainBoost:
                            description.text = "CHAIN BOOST" + (seg.giftQuantity != 1 ? "S" : "");
                            break;
                        case GiftType.EnduranceBoost:
                            description.text = "ENDURANCE BOOST" + (seg.giftQuantity != 1 ? "S" : "");
                            break;
                        case GiftType.SuperBoost:
                            description.text = "SUPERCHARGE BOOST" + (seg.giftQuantity != 1 ? "S" : "");
                            break;
                        case GiftType.ScoreBoost:
                            description.text = "SCORE SEEKER BOOST" + (seg.giftQuantity != 1 ? "S" : "");
                            break;
                        case GiftType.ShrinkBoost:
                            description.text = "SHRINK BOOST" + (seg.giftQuantity != 1 ? "S" : "");
                            break;
                        case GiftType.EnlargeBoost:
                            description.text = "ENLARGE BOOST" + (seg.giftQuantity != 1 ? "S" : "");
                            break;
                        default:
                            description.text = "Invalid prize type: " + seg.giftType;
                            break;
                    }

                    description.GetComponent<Bounce>()?.DoBounce();
                }

                prevSeg = seg;
            }
        }

        public void Spin()
        {
            timeRemaining.Deactivate();
            itemAwarded.Activate();
            spinning = true;
            speed = minSpeed + Random.value * (maxSpeed-minSpeed);
            slowMod = 1f;
            backButton.Interactable = false;//.gameObject.Deactivate();
            IsSkippable = true;
        }

        private void StopSpinning()
        {
            spinning = false;

            AddPrize();
            
            Invoke("AnimRewardScreen", 1f);
        }

        private void AddPrize()
        {
            GiftWheelSegment seg = GetSelectedSegment();

            // Add rewards to totals
            switch (seg.giftType)
            {
                case GiftType.Rotorpoints:
                    PlayerFile plrFile = PlayerFile.GetFile();
                    plrFile.RotorPoints += seg.giftQuantity;
                    PlayerFile.file = plrFile;
                    PlayerFile.SaveFile();
                    break;
                case GiftType.ChainBoost:
                    chainBoost.AddPrize(seg.giftQuantity);
                    break;
                case GiftType.EnduranceBoost:
                    enduranceBoost.AddPrize(seg.giftQuantity);
                    break;
                case GiftType.SuperBoost:
                    superBoost.AddPrize(seg.giftQuantity);
                    break;
                case GiftType.ScoreBoost:
                    scoreBoost.AddPrize(seg.giftQuantity);
                    break;
                case GiftType.ShrinkBoost:
                    shrinkBoost.AddPrize(seg.giftQuantity);
                    break;
                case GiftType.EnlargeBoost:
                    enlargeBoost.AddPrize(seg.giftQuantity);
                    break;
                default:
                    print("Invalid prize type: " + seg.giftType);
                    break;
            }
        }

        public void DoublePrize()
        {
            AddPrize();
            GiftWheelSegment seg = GetSelectedSegment();
            foreach (Text quantity in giftQuantities)
            {
                quantity.GetComponent<CountUp>()?.Count(new Vector2Int(seg.giftQuantity,seg.giftQuantity * 2),Mathf.Sqrt(seg.giftQuantity)/20f);
            }
        }

        private void AnimRewardScreen()
        {
            animator.SetTrigger("Reward Screen");
            //animator.Play("Gift Claimed");
        }

        public bool CanSkip()
        {
            return spinning && (speed / minSpeed) > 0.5f;
        }

        public void SkipSpin()
        {
            SetAngle(Random.value * 2 * Mathf.PI);
            StopSpinning();
        }

        public void ExtraSpin()
        {
            extraSpin = true;
        }
    }
}


//   4 segments, 0.5pi per segment  0.25pi per half segment
//   segment 0 is greater than 1.75pi OR  less than 0.25pi
//   segment 1 is greater than 0.25pi AND less than 0.75pi
//   segment 2 is greater than 0.75pi AND less than 1.25pi
//   segment 3 is greater than 1.25PI AND less than 1.75pi
//
//   angle = 0.2pi
//   0.2pi/0.5pi = 0.4
//   angle = 1.6pi
//   1.6pi/0.5pi = 3.2
//   angle = 1.75pi = 3.5
//   angle = 0 = 0
//   angle = 1.9/0.5 = 3.8



//   angle = 1.76 => 3.52
//   angle = 0.24 => 0.48

//   angle = 0.26 => 0.52
//   angle = 0.74 => 1.48

//   angle = 0.76 => 1.52
//   angle = 1.24 => 2.48

//   angle = 1.26 => 2.52
//   angle = 1.74 => 3.48


//   (int)(angle + half_seg / segment_size) = index(ish)