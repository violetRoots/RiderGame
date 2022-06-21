using RiderGame.SO;

namespace RiderGame.Level
{
    public class RuntimeLevelData
    {
        public float YSpeed => _ySpeed;
        public float XSpeed => _levelConfig.XSpeed;

        private LevelConfiguration _levelConfig;

        private float _ySpeed;

        public void Init(LevelConfiguration levelConfiguration)
        {
            _levelConfig = levelConfiguration;
            _ySpeed = 0;
        }

        public void SetYSpeed(float speed)
        {
            _ySpeed = speed;
        }
    }
}