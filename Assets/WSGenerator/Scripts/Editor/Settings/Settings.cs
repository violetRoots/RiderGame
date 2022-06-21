using UnityEngine;
using UnityEditor;
using NaughtyAttributes;

namespace SkyCrush.WSGenerator
{
    [CreateAssetMenu(fileName = "WSG_Settings", menuName = "WSGenerator/Settings", order = 0)]
    public partial class Settings : SingletonSOEditorOnly<Settings>
    {
        [SerializeField]
        private float frequencySecondsPerUnit = 5.0f;
        [SerializeField]
        private float minFrequencyGenerationValue = 0.01f;        
        [SerializeField]
        private float maxFrequencyGenerationValue = 100.0f;

        [Header("Area Settings")]
        [SerializeField]
        private AreaContainer areaContainer;
    }
}
