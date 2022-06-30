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
        public AreaSettings AreaSettings => areaSettings;
        public PoolSettings PoolSettings => poolSettings;
        public SpawnPointer2D SpawnPointer2D => spawnPointer2D;

        public static void Select()
        {
            Selection.activeObject = Instance;
        }
    }
}
