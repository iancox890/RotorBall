using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PsychedelicGames.RotorBall.UI
{
    [RequireComponent(typeof(MaskableGraphic))]
    public class Bounce : MonoBehaviour
    {
        // TODO: Allow bounce size lower than 1
        [SerializeField] [Range(1f,2f)] private float bounceSize = 1.1f;
        [SerializeField] [Range(0.01f,0.5f)] private float bounceTime = 0.1f;
        public float timer;
        public bool bouncing;
        public bool rebounding;
        

        private MaskableGraphic graphic;

        // Start is called before the first frame update
        void Start()
        {
            GetComponent<MaskableGraphic>();
        }

        // Update is called once per frame
        void Update()
        {
            if (bouncing || rebounding) { timer += Time.deltaTime / bounceTime; }
            if (timer >= 1f)
            {
                if (bouncing)
                {
                    bouncing = false;
                    rebounding = true;
                } else
                {
                    rebounding = false;
                    timer = 0f;
                }
            } else
            {
                float size = Mathf.Lerp(1f, bounceSize, timer);
                Vector3 scale = transform.localScale;
                scale.x = size;
                scale.y = size;
                transform.localScale = scale;
            }
        }

        public void DoBounce()
        {
            bouncing = true;
            rebounding = false;
            timer = 0f;
        }
    }
}
