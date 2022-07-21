namespace RiderGame.RuntimeData
{
    public class SessionRuntimeData
    {
        public float CoinsCount { get; private set; }

        public void SetCoinsCount(float value)
        {
            CoinsCount = value;
        }
    }
}
