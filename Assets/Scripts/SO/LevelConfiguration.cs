using System;
using UnityEngine;

namespace RiderGame.SO
{
    [CreateAssetMenu(fileName = "LevelConfigs", menuName = "RiderGame/LevelConfigs")]
    public class LevelConfiguration : ScriptableObject
    {
        [Header("Static")]
        public float baseXSpeed = 5.0f;

        [Header("Runtime")]
        public LevelStage[] fixedStages;
        public LevelStage[] randomStages;
    }

    [Serializable]
    public class LevelStage
    {
        public float duration = 10.0f;

        [Header("World")]
        public float maxYSpeed = 5.0f;
        public AnimationCurve ySpeedCurve;

        [Space(10)]
        public SpawnWorldObjectInfo[] obstacles;
    }

    [Serializable]
    public class SpawnWorldObjectInfo
    {
        public GameObject spawnObject;

        public int maxCount = 1;
        public AnimationCurve countCurve;
    }
}
