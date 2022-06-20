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
        private bool useCustomTimeInterval = true;

        [ShowIf(nameof(useCustomTimeInterval))]
        [SerializeField]
        private float updateStageInterval = 0.01f;

        [Header("Area Settings")]
        [Expandable]
        [SerializeField]
        private AreaContainer areaContainer;
    }
}
