using UnityEngine;
using NaughtyAttributes;
using SkyCrush.Utility;

namespace SkyCrush.WSGenerator
{
#if UNITY_EDITOR
    public class AreaConfigurationUpdater : MonoBehaviour
    {
        [Button("Settings")]
        [SerializeField]
        private void GoToSettings() => Settings.Select();

        private void OnDrawGizmos()
        {
            foreach(var area in Settings.Instance.AreaContainer.areas)
            {
                if (!area.drawGizmos) continue;

                CustomGizmos.DrawRect(area.center, area.size, area.color);
            }
        }
    }
#endif
}
