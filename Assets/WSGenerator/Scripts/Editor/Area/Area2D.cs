using UnityEngine;

namespace SkyCrush.WSGenerator
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Area2D : MonoBehaviour
    {
        public AreaInfo Info => _info;
        public BoxCollider2D BoxCollider
        {
            get
            {
                if(_boxCollider == null)
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
    }
}
