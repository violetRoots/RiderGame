using UnityEngine;
using SkyCrush.Utility;
using System;

namespace SkyCrush.WSGenerator
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpawnPointer2D : MonoBehaviour
    {
        public bool ReadyToSpawn => _contactsWithBounds == 0;

        public BoxCollider2D BoxCollider
        {
            get
            {
                if (_boxCollider == null)
                    _boxCollider = GetComponent<BoxCollider2D>();
                return _boxCollider;
            }
        }

        private Action OnTriggerWithBounds;

        private BoxCollider2D _boxCollider;
        private int _contactsWithBounds;

        public void Init(SpawnBounds2D instanceBounds, Action OnTriggerWithBounds)
        {
            this.OnTriggerWithBounds = OnTriggerWithBounds;

            BoxCollider.offset = instanceBounds.BoxCollider.offset;
            BoxCollider.size = instanceBounds.BoxCollider.size;
        }

        private void FixedUpdate()
        {
            if(_contactsWithBounds > 0)
            {
                OnTriggerWithBounds?.Invoke();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<SpawnBounds2D>() == null && collision.GetComponent<SpawnPointer2D>() == null) return;

            _contactsWithBounds++;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<SpawnBounds2D>() == null && collision.GetComponent<SpawnPointer2D>() == null) return;

            _contactsWithBounds--;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            CustomGizmos.DrawRect(transform.position + (Vector3)BoxCollider.offset, BoxCollider.size, Color.yellow);
        }
#endif
    }
}
