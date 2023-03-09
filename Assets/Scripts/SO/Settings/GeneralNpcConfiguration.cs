using UnityEngine;

namespace RiderGame.SO
{
    [CreateAssetMenu(fileName = "GeneralNpcConfigs", menuName = "RiderGame/GeneralNpcConfigs", order = 2)]
    public class GeneralNpcConfiguration : ScriptableObject
    {
        public float PushForce => pushForce;
        public float PushTime => pushTime;

        [Header("Collision")]
        [SerializeField]
        private float pushForce = 1.0f;
        [SerializeField]
        private float pushTime = 0.1f;
    }
}
