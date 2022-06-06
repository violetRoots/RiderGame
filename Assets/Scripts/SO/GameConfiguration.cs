using UnityEngine;

namespace RiderGame.SO
{
    [CreateAssetMenu(fileName = "GameConfigs", menuName = "RiderGame/GameConfigs")]
    public class GameConfiguration : SingletonScriptableObject<GameConfiguration>
    {
        public LevelConfiguration[] levelConfigs;
    }
}
