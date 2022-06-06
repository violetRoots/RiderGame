using UnityEngine;
using RiderGame.SO;
using NaughtyAttributes;
using UnityEditor;

namespace RiderGame.CustomGizmos
{
    public class GenerateAreasDrawer : MonoBehaviour
    {
        [Button("Edit Area Configs")]
        private void EditAreaConfigs() => GenerateAreaConfiguration.Select();

        private void OnDrawGizmos()
        {
            foreach(var area in GenerateAreaConfiguration.Instance.generateAreas)
            {
                if (!area.drawGizmos) continue;

                DrawGenerateArea(area);
            }
        }

        private void DrawGenerateArea(GenerateAreaInfo area)
        {
            var point11 = new Vector2(-area.point1.x, area.point1.y);
            var point12 = new Vector2(-area.point1.x, area.point2.y);
            var point21 = new Vector2(-area.point2.x, area.point1.y);
            var point22 = new Vector2(-area.point2.x, area.point2.y);

            Gizmos.color = area.color;
            Gizmos.DrawLine(point11, point12);
            Gizmos.DrawLine(point12, point22);
            Gizmos.DrawLine(point22, point21);
            Gizmos.DrawLine(point21, point11);
        }
    }
}
