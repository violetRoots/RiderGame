using UnityEngine;

namespace RiderGame.Editor.Debug
{
    public class DebugPanelsController : MonoBehaviour
    {
        private DebugStageInfoPanel _stageInfoPanel;

        private void Awake()
        {
            _stageInfoPanel = GetComponentInChildren<DebugStageInfoPanel>(true);
        }

        void Update()
        {
            if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (_stageInfoPanel == null) return;

                _stageInfoPanel.gameObject.SetActive(!_stageInfoPanel.gameObject.activeSelf);
            }
        }
    }
}
