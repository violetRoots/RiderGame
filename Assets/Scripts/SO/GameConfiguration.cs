using UnityEngine;
using SkyCrush.Utility;

namespace RiderGame.SO
{
    [CreateAssetMenu(fileName = "GameConfigs", menuName = "RiderGame/GameConfigs", order = 0)]
    public class GameConfiguration : SingletonConfiguration<GameConfiguration>
    {
        public PlayerConfiguration PlayerConfiguration => playerConfiguration;
        public float MaxActiveObjectPosition => maxActiveObjectPosition;
        public float ChangeLayerEdge => changeLayerEdgePosition;
        public float MouseDirectionSensitivity => mouseDirectionSensitivity;
        public float TouchDirectionSensitivity => touchDirectionSensitivity;
        public float ClampDirectionAngle => clampDirectionAngle;
        public LevelConfiguration[] Levels => _levelConfigs;

        [Header("World")]
        [SerializeField]
        private float maxActiveObjectPosition = 100.0f;
        [SerializeField]
        private float changeLayerEdgePosition = 0.0f;

        [Header("Gameplay")]
        [SerializeField]
        private PlayerConfiguration playerConfiguration;
        [Space(10)]
        [SerializeField]
        private float mouseDirectionSensitivity = 0.1f;
        [SerializeField]
        private float touchDirectionSensitivity = 0.5f;
        [SerializeField]
        private float clampDirectionAngle = 60.0f;

        [Space(10)]
        [SerializeField]
        private LevelConfiguration[] _levelConfigs;
    }
}
