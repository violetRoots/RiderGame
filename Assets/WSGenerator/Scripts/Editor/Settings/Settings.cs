using UnityEngine;

namespace SkyCrush.WSGenerator
{
    [CreateAssetMenu(fileName = "WSG_Settings", menuName = "WSGenerator/Settings", order = 0)]
    public partial class Settings : SingletonSettings<Settings>
    {
        [SerializeField]
        private float frequencySecondsPerUnit = 5.0f;
        [SerializeField]
        private float minFrequencyGenerationValue = 0.01f;        
        [SerializeField]
        private float maxFrequencyGenerationValue = 100.0f;

        [Header("Area Settings")]
        [SerializeField]
        private AreaConfiguration areaContainer;
    }
}
