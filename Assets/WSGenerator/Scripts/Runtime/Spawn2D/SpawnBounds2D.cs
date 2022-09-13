using UnityEngine;
using SkyCrush.Utility;

namespace SkyCrush.WSGenerator
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class SpawnBounds2D : MonoBehaviour
    {
        public BoxCollider2D BoxCollider
        {
            get
            {
                if (_boxCollider == null)
                    _boxCollider = GetComponent<BoxCollider2D>();
                return _boxCollider;
            }
        }

        private BoxCollider2D _boxCollider;

#if UNITY_EDITOR

        private int _areaContacts;

        private void OnDrawGizmos()
        {
            if ((Application.isEditor && !Application.isPlaying) || (Application.isPlaying && _areaContacts > 0))
            {
                CustomGizmos.DrawRect(transform.position + (Vector3) BoxCollider.offset, BoxCollider.size * transform.lossyScale, Color.red);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out Area2D area)) return;

            _areaContacts++;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out Area2D area)) return;

            _areaContacts--;
        }
#endif
    }
}
