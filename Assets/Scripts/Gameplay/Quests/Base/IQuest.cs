using UniRx;

namespace RiderGame.Gameplay
{
    public interface IQuest
    {
        public ReactiveProperty<QuestStatus> Status { get; set; }
    }

    public enum QuestStatus
    {
        NotStarted = 0,
        InProgress = 1,
        Completed = 2,
        Failed = 3
    }
}
