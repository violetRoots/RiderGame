namespace RiderGame.RuntimeData
{
    public class SessionRuntimeData
    {
        public int CoinsCount { get; private set; }

        public void SetCoinsCount(int value)
        {
            CoinsCount = value;
        }
    }
}
