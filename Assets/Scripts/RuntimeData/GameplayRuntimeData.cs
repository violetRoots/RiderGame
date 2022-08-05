using RiderGame.SO;

namespace RiderGame.RuntimeData
{
    public class GameplayRuntimeData
    {
        public float MovementSpeed => _movementSpeed;
        public float MovementDirection => _movementDirection;

        private LevelConfiguration _levelConfig;

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