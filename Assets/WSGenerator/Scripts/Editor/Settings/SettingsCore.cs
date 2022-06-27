using UnityEngine;
using UnityEditor;
using NaughtyAttributes;

namespace SkyCrush.WSGenerator
{
    public partial class Settings
    {
        public float FrequencySecondsPerUnit => frequencySecondsPerUnit;
        public float MinFrequencyGenerationValue => minFrequencyGenerationValue;
        public float MaxFrequencyGenerationValue => maxFrequencyGenerationValue;
        public AreaConfiguration AreaContainer => areaContainer;

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
