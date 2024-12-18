using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIImageFader : MonoBehaviour
    {
        private Image image;
        public float opacityDelta = 0.04f;
        private float speed = 0;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        private void Update()
        {
            Color tempColor = image.color;
            tempColor.a = Mathf.Clamp((tempColor.a + speed * opacityDelta), 0, 1.0f);
            image.color = tempColor;
        }

        /// <summary>
        /// Trigger CharacterImage to Fade in or out.
        /// </summary>
        /// <param name="dir">true: Fade in. false: Fadeout.</param>
        public void TriggerFade(bool dir, bool restart = false)
        {
            if (restart)
            {
                Color tempColor = image.color;
                tempColor.a = (dir ? 0f : 1.0f);
                image.color = tempColor;
            }
            speed = (dir ? 1.0f : -1.0f);
        }

        public void StopFade()
        {
            speed = 0;
        }
    }
}
