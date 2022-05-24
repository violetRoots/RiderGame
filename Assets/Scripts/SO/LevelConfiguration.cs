using System;
using UnityEngine;

namespace RiderGame.SO
{
    [CreateAssetMenu(fileName = "LevelConfigs", menuName = "RiderGame/LevelConfigs")]
    public class LevelConfiguration : ScriptableObject
    {
        public LevelStage[] fixedStages;
        public LevelStage[] randomStages;
    }

    [Serializable]
    public class LevelStage
    {
        public float duration = 10.0f;

        [Header("World")]
        public float maxSpeed = 300.0f;
        public AnimationCurve speedCurve;
    }
}
