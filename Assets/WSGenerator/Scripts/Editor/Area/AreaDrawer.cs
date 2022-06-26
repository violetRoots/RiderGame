using UnityEngine;
using NaughtyAttributes;

namespace SkyCrush.WSGenerator
{
    public class AreaDrawer : MonoBehaviour
    {
        [Button("Settings")]
        [SerializeField]
        private void GoToSettings() => Settings.Select();

        private void OnDrawGizmos()
        {
            foreach(var area in Settings.Instance.AreaContainer.areas)
            {
                if (!area.drawGizmos) continue;

                DrawGenerateArea(area);
            }
        }

        private void DrawGenerateArea(AreaInfo area)
        {
            var point1 = area.center + (new Vector2(-1, -1) * area.size/2);
            var point2 = area.center + (new Vector2(-1, 1) * area.size / 2);
            var point3 = area.center + (new Vector2(1, 1) * area.size / 2);
            var point4 = area.center + (new Vector2(1, -1) * area.size / 2);

            Gizmos.color = area.color;
            Gizmos.DrawLine(point1, point2);
            Gizmos.DrawLine(point2, point3);
            Gizmos.DrawLine(point3, point4);
            Gizmos.DrawLine(point4, point1);
        }
    }
}
