using SkyCrush.WSGenerator;
using UnityEngine;
using TMPro;

namespace RiderGame.Editor.DebugUI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class DebugStageInfoPanel : MonoBehaviour
    {
        private Generator _generator;
        private TextMeshProUGUI _stageInfoText;

        private void Awake()
        {
            _stageInfoText = GetComponent<TextMeshProUGUI>();

            var generators = FindObjectsOfType<Generator>();
            if(generators.Length > 0)
            {
                _generator = generators[0];
            }
        }

        private void Update()
        {
            if (_generator == null || !_generator.IsInitilized) return;

            _stageInfoText.text = _generator.GetDebugStageInfo();
        }
    }
}
