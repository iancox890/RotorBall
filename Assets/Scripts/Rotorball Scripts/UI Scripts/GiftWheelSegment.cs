using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace PsychedelicGames.RotorBall
{
    [System.Serializable]
    public enum GiftType { Rotorpoints, ChainBoost, EnduranceBoost, SuperBoost, ScoreBoost, ShrinkBoost, EnlargeBoost }

    public class GiftWheelSegment : MonoBehaviour
    {
        [SerializeField] public GiftType giftType;
        [SerializeField] public int giftQuantity;
        [Header("References")]
        [SerializeField] private Image _segmentImage;
        [SerializeField] private Image _giftImage;
        [SerializeField] private Text _giftQuantityText;

        private void Awake()
        {
            if (_segmentImage == null || _giftImage == null) {
                foreach (Image img in GetComponentsInChildren<Image>())
                {
                    if (img.name.Equals("Segment Image"))
                    {
                        // print($"{gameObject.name} found segment image {segmentImage==null}");
                        _segmentImage = _segmentImage ?? img;
                    } else if (img.name.Equals("Item Image"))
                    {
                        _giftImage = _giftImage ?? img;
                    }
                }
            }
            _giftQuantityText = _giftQuantityText ?? GetComponentInChildren<Text>();
            _giftQuantityText.text = giftQuantity.ToString();
        }

        public void SetMaterials(Material segmentMat, Material giftImgMat, Material giftQuantityMat)
        {
            if (_segmentImage != null) {
                _segmentImage.material = segmentMat;
                // print($"Segment image of segment {gameObject.name} is null");
                // return;  
            }
            
            // if (giftImage == null) {
            //     print($"Gift image of segment {gameObject.name} is null");
            //     return;
            // }
            _giftImage.material = giftImgMat;
            _giftQuantityText.material = giftQuantityMat;
        }

        public Sprite GetImage()
        {
            return _giftImage.sprite;
        }
    }
}