using RiderGame.World;
using UnityEditor;
using UnityEngine;

namespace RiderGame.Editor.CustomGizmos
{
    public class ObjectLayerDrawer : BaseGizmosDrawer
    {
#if UNITY_EDITOR
        private const float XPointPos = 15.0f;

        private Overlay _overlayValue;

        public void UpdateValue(Overlay newValue)
        {
            _overlayValue = newValue;
        }

        private void OnDrawGizmos()
        {
            if (Selection.activeGameObject != gameObject) return;

            if (!Application.isPlaying)
            {
                _overlayValue = GetComponent<OverlayComponent>().Value;
            }

            var yPos = transform.position.y + _overlayValue.layerEdgeOffset;
            Vector3 left = new Vector3(transform.position.x - XPointPos, yPos, 0);
            Vector3 right = new Vector3(transform.position.x + XPointPos, yPos, 0);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(left, right);
        }
#endif
    }
}
