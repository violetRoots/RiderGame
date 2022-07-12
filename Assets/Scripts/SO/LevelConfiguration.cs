using UnityEngine;
using SkyCrush.WSGenerator;

namespace RiderGame.SO
{
    [CreateAssetMenu(fileName = "LevelConfigs", menuName = "RiderGame/LevelConfigs")]
    public class LevelConfiguration : ScriptableObject
    {
        [Header("Runtime")]
        [Space(5)]

        [SerializeField]
        private Sequence sequence;
    }
}
