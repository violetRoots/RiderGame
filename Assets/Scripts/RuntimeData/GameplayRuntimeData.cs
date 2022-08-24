using System.Collections.ObjectModel;
using System.ComponentModel;
using RiderGame.Gameplay;
using RiderGame.SO;

namespace RiderGame.RuntimeData
{
    public class GameplayRuntimeData
    {
        public ObservableCollection<IQuest> Quests => _quests;
        public float MovementSpeed => _movementSpeed;
        public float MovementDirection => _movementDirection;

        private LevelConfiguration _levelConfig;

        private readonly ObservableCollection<IQuest> _quests = new ObservableCollection<IQuest>();
        private float _movementSpeed;
        private float _movementDirection;

        public void Init(LevelConfiguration levelConfiguration)
        {
            _levelConfig = levelConfiguration;
            _movementSpeed = 0;
            _movementDirection = 0;
        }

        public void SetMovementSpeed(float speed)
        {
            _movementSpeed = speed;
        }

        public void SetMovementDirection(float direction)
        {
            _movementDirection = direction;
        }
    }
}