using RiderGame.SO;
using UnityEngine;

namespace RiderGame.Editor.CustomGizmos
{
    public class ChangeLayerEdgeDrawer : MonoBehaviour
    {
#if UNITY_EDITOR
        private const float XPointPos = 1000.0f;

        private void OnDrawGizmos()
        {
            Vector3 left = new Vector3(-XPointPos, GameConfiguration.Instance.ChangeLayerEdge, 0);
            Vector3 right = new Vector3(XPointPos, GameConfiguration.Instance.ChangeLayerEdge, 0);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(left, right);
        }
#endif
    }
}
