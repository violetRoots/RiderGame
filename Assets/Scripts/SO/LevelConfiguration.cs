using UnityEngine;
using NaughtyAttributes;
using SkyCrush.WSGenerator;

namespace RiderGame.SO
{
    [CreateAssetMenu(fileName = "LevelConfigs", menuName = "RiderGame/LevelConfigs")]
    public class LevelConfiguration : ScriptableObject
    {
        public float XSpeed => baseXSpeed;

        [Header("Static")]
        [SerializeField]
        private float baseXSpeed = 5.0f;

        [Header("Runtime")]
        [Space(5)]

        [SerializeField]
        [Expandable]
        private Sequence sequence;
    }
}
