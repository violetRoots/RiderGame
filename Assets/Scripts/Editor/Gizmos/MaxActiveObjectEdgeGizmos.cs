using UnityEngine;
using RiderGame.SO;

namespace RiderGame.Editor.CustomGizmos
{
    public class MaxActiveObjectEdgeGizmos : MonoBehaviour
    {
#if UNITY_EDITOR
        private const float XPointPos = 1000.0f;

        private void OnDrawGizmos()
        {
            Vector3 left = new Vector3(-XPointPos, GameConfiguration.Instance.MaxActiveObjectPosition, 0);
            Vector3 right = new Vector3(XPointPos, GameConfiguration.Instance.MaxActiveObjectPosition, 0);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(left, right);
        }
#endif
    }
}
