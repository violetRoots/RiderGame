using UnityEngine;
using NaughtyAttributes;

namespace RiderGame.SO
{
    [CreateAssetMenu(fileName = "LevelConfigs", menuName = "RiderGame/LevelConfigs")]
    public class LevelConfiguration : ScriptableObject
    {
        [Header("Static")]
        public float baseXSpeed = 5.0f;

        [Header("Runtime")]
        [Space(5)]
        [NonReorderable]
        public GameObject[] poolObjects;

        [ReorderableList]
        public LevelStage[] fixedStages;
        [ReorderableList]
        public LevelStage[] randomStages;

        private void OnValidate()
        {
            UpdateStageValues();
        }

        private void UpdateStageValues()
        {
            var generateAreasConfigs = GenerateAreaConfiguration.Instance;

            foreach (var fixedStage in fixedStages)
            {
                fixedStage.UpdatePoolObjects(ref poolObjects);
                fixedStage.UpdateGenerateAreas(ref generateAreasConfigs.generateAreas);
            }

            foreach (var randomStage in randomStages)
            {
                randomStage.UpdatePoolObjects(ref poolObjects);
                randomStage.UpdateGenerateAreas(ref generateAreasConfigs.generateAreas);
            }
        }
    }
}
