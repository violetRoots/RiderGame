using UnityEngine;

namespace RiderGame.SO
{
    [CreateAssetMenu(fileName = "GameConfigs", menuName = "RiderGame/GameConfigs")]
    public class GameConfiguration : SingletonSOEditorOnly<GameConfiguration>
    {
        public LevelConfiguration[] levelConfigs;
    }
}
