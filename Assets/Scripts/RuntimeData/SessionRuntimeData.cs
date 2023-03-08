using UniRx;

namespace RiderGame.RuntimeData
{
    public class SessionRuntimeData
    {
        public ReactiveProperty<SessionStatus> Status { get; private set; } = new ReactiveProperty<SessionStatus>();
        public ReactiveProperty<int> LifesCount { get; private set; } = new ReactiveProperty<int>();
        public ReactiveProperty<int> CoinsCount { get; private set; } = new ReactiveProperty<int>();
    }

    public enum SessionStatus
    {
        NotStarted = 0,
        Playing = 1,
        Paused = 2,
        Ended = 3
    }
}
