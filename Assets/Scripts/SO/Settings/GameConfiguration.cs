using UnityEngine;
using SkyCrush.Utility;

namespace RiderGame.SO
{
    [CreateAssetMenu(fileName = "GameConfigs", menuName = "RiderGame/GameConfigs", order = 0)]
    public class GameConfiguration : SingletonConfiguration<GameConfiguration>
    {
        public const float ClampDirectionAngle = 60.0f;

        public float MouseDirectionSensitivity => mouseDirectionSensitivity;
        public float TouchDirectionSensitivity => touchDirectionSensitivity;
        public float TapDetectTime => tapDetectTime;
        public float MaxTapOffset => maxTapOffset;
        public float SwipeDetectTime => swipeDetectTime;
        public float SwipeSensitivity => swipeSensitivity;

        [Header("Input")]
        [SerializeField]
        private float mouseDirectionSensitivity = 0.1f;
        [SerializeField]
        private float touchDirectionSensitivity = 0.5f;
        [SerializeField]
        private float tapDetectTime = 0.1f;
        [SerializeField]
        private float maxTapOffset = 0.05f;
        [SerializeField]
        private float swipeDetectTime = 0.2f;
        [Range(0.0f, 1.0f)]
        [SerializeField]
        private float swipeSensitivity = 0.2f;

        public PlayerConfiguration PlayerConfiguration => playerConfiguration;

        [Header("Gameplay")]
        [SerializeField]
        private PlayerConfiguration playerConfiguration;

        public float MaxActiveObjectPosition => maxActiveObjectPosition;
        public float BackgroundSpeedMultiplier => backgroundSpeedMultiplier;
        public float ChangeLayerEdge => changeLayerEdgePosition;
        public LevelConfiguration[] Levels => _levelConfigs;

        [Header("World")]
        [SerializeField]
        private float maxActiveObjectPosition = 100.0f;
        [SerializeField]
        private float backgroundSpeedMultiplier = 0.00035f;
        [SerializeField]
        private float changeLayerEdgePosition = 0.0f;

        [Space(10)]
        [SerializeField]
        private LevelConfiguration[] _levelConfigs;
    }
}
