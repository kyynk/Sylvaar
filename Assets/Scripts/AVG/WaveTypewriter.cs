using UnityEngine;

namespace AVG
{
    public class WaveTypewriter : ITypewriterStrategy
    {
        private readonly float typeSpeed;
        private readonly float waveAmplitude;

        public WaveTypewriter(float typeSpeed = 0.05f, float amplitude = 2f)
        {
            this.typeSpeed = typeSpeed;
            this.waveAmplitude = amplitude;
        }

        public override string ProcessText(string fullText, int currentIndex, float elapsedTime)
        {
            // Play Audio effect here.
            return fullText.Substring(0, currentIndex);
        }

        public override string PrepareText(string fullText, int currentIndex, float elapsedTime)
        {
            if (currentIndex == 0)
            {
                return $"<voffset={Mathf.Sin(elapsedTime * 10) * waveAmplitude}em>" +
                       $"<size={elapsedTime * 2500}%>{fullText.Substring(0, 1)}</size>" +
                       $"</voffset>";
            }
            else if (currentIndex < fullText.Length)
            {
                var textFront = fullText.Substring(0, currentIndex);
                var textBack = fullText.Substring(currentIndex, 1);
                // Add wave effect here.
                // for typeSpeed 0.1
                // return $"{textFront}<voffset=" +
                //        $"{Mathf.Sin(elapsedTime * 10 + Mathf.PI / 2) * waveAmplitude}" +
                //        $"em><size={250 - elapsedTime * 2500}%>{textBack}</size></voffset>";

                // for typeSpeed 0.05
                return $"{textFront}<voffset={Mathf.Sin(elapsedTime * 10) * waveAmplitude}em>" +
                       $"<size={elapsedTime * 2500}%>{textBack}</size>" +
                       $"</voffset>";
            }
            else
            {
                return fullText;
            }
        }

        public override bool IsComplete(int currentIndex, string fullText)
        {
            return currentIndex >= fullText.Length;
        }

        public override float GetTypeSpeed() => typeSpeed;
    }
}