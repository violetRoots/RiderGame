using UnityEngine;

namespace RiderGame.SO
{
    [CreateAssetMenu(fileName = "GameConfigs", menuName = "RiderGame/GameConfigs")]
    public class GameConfiguration : SingletonSOEditorOnly<GameConfiguration>
    {
        public float MaxActiveObjectPosition => maxActiveObjectPosition;
        public LevelConfiguration[] Levels => _levelConfigs;

        [Header("Global")]
        [SerializeField]
        private float maxActiveObjectPosition = 100.0f;

        [Space(10)]
        [SerializeField]
        private LevelConfiguration[] _levelConfigs;
    }
}
