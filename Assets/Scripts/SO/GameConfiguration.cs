using UnityEngine;
using SkyCrush.Utility;

namespace RiderGame.SO
{
    [CreateAssetMenu(fileName = "GameConfigs", menuName = "RiderGame/GameConfigs")]
    public class GameConfiguration : SingletonConfiguration<GameConfiguration>
    {
        public float MaxActiveObjectPosition => maxActiveObjectPosition;
        public float ChangeLayerEdge => changeLayerEdgePosition;
        public LevelConfiguration[] Levels => _levelConfigs;

        [Header("Global")]
        [SerializeField]
        private float maxActiveObjectPosition = 100.0f;
        [SerializeField]
        private float changeLayerEdgePosition = 0.0f;

        [Space(10)]
        [SerializeField]
        private LevelConfiguration[] _levelConfigs;
    }
}
