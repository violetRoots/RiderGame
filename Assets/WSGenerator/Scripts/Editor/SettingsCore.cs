using UnityEngine;
using UnityEditor;
using NaughtyAttributes;

namespace SkyCrush.WSGenerator
{
    public partial class Settings
    {
        public float FrequencySecondsPerUnit => frequencySecondsPerUnit;
        public float MinFrequencyGenerationValue => minFrequencyGenerationValue;
        public bool UseCustomTimeInterval => useCustomTimeInterval;
        public float UpdateStageInterval => updateStageInterval;
        public AreaContainer AreaContainer => areaContainer;

        public static void Select()
        {
            Selection.activeObject = Instance;
        }

        public static void SelectAreaContainer()
        {
            Selection.activeObject = Instance.AreaContainer;
        }
    }
}
