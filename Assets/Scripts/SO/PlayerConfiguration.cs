using UnityEngine;

namespace RiderGame.SO
{
    [CreateAssetMenu(fileName = "PlayerConfigs", menuName = "RiderGame/PlayerConfigs")]
    public class PlayerConfiguration : ScriptableObject
    {
        public float baseSpeed = 100.0f;
    }
}
