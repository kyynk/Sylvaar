namespace AVG
{
    public class NormalTypewriter : ITypewriterStrategy
    {
        private readonly float typeSpeed;

        public NormalTypewriter(float typeSpeed = 0.05f)
        {
            this.typeSpeed = typeSpeed;
        }

        public override string ProcessText(string fullText, int currentIndex, float elapsedTime)
        {
            return fullText.Substring(0, currentIndex);
        }

        public override string PrepareText(string fullText, int currentIndex, float elapsedTime)
        {
            return fullText.Substring(0, currentIndex);
        }

        public override bool IsComplete(int currentIndex, string fullText)
        {
            return currentIndex >= fullText.Length;
        }

        public override float GetTypeSpeed() => typeSpeed;
    }
}
