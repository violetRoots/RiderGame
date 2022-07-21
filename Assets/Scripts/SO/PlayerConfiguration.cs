using UnityEngine;

namespace RiderGame.SO
{
    [CreateAssetMenu(fileName = "PlayerConfigs", menuName = "RiderGame/PlayerConfigs", order = 2)]
    public class PlayerConfiguration : ScriptableObject
    {
        public float PushForce => pushForce;
        public float PushTime => pushTime;
        public float InvunerabilityDuration => invunerabilityDuration;
        public float InvunerabilityBlinkInterval => invunerabilityBlinkInterval;

        [Header("Collision with obstacle")]
        [SerializeField]
        private float pushForce = 3.0f;
        [SerializeField]
        private float pushTime = 0.1f;
        [SerializeField]
        private float invunerabilityDuration = 2.0f;
        [SerializeField]
        private float invunerabilityBlinkInterval = 0.25f;
    }
}
