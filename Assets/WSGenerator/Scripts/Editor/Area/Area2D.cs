using SkyCrush.Utility;
using UnityEngine;

namespace SkyCrush.WSGenerator
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Area2D : MonoBehaviour
    {
        public AreaInfo Info => _info;
        public Vector3 Center => transform.position + (Vector3)BoxCollider.offset;
        public Vector3 Size => BoxCollider.size;

        private BoxCollider2D BoxCollider
        {
            get
            {
                if (_boxCollider == null)
                    _boxCollider = GetComponent<BoxCollider2D>();
                return _boxCollider;
            }
        }

        private AreaInfo _info;
        private BoxCollider2D _boxCollider;

        public void Init(AreaInfo areaInfo)
        {
            _info = areaInfo;

            transform.position = areaInfo.center;
            BoxCollider.size = areaInfo.size;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!Info.drawGizmos) return;

            CustomGizmos.DrawRect(Center, Size, Info.color);
        }
#endif
    }
}
