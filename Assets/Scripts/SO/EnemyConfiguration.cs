using UnityEngine;

namespace RiderGame.SO
{
    [CreateAssetMenu(fileName = "Enemy_1", menuName = "RiderGame/Enemy", order = 3)]
    public class EnemyConfiguration : ScriptableObject
    {
        public float ClampAngle => clampAngle;
        public float MovementSpeed => movementSpeed;
        public float PushForce => pushForce;
        public float PushTime => pushTime;

        [SerializeField]
        private float clampAngle;
        [SerializeField]
        private float movementSpeed = 10.0f;
        [SerializeField]
        private float pushForce = 1.0f;
        [SerializeField]
        private float pushTime = 0.1f;
    }
}