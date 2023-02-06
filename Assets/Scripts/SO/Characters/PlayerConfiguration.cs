using UnityEngine;
using NaughtyAttributes;
using RiderGame.Gameplay;

namespace RiderGame.SO
{
    [CreateAssetMenu(fileName = "PlayerConfigs", menuName = "RiderGame/PlayerConfigs", order = 2)]
    public class PlayerConfiguration : ScriptableObject
    {
        public float DashDetectTime => dashDetectTime;
        public float DashDistance => dashDistance;
        public float DashTime => dashTime;
        public float DashCooldown => dashCooldown;

        public float PushForce => pushForce;
        public float PushTime => pushTime;
        public float InvunerabilityDuration => invunerabilityDuration;
        public float InvunerabilityBlinkInterval => invunerabilityBlinkInterval;
        public CoinComponent CoinPrefab => coinPrefab;
        public Vector2Int CoinsDropCount => coinsDropCount;
        public float CoinsDropRadius => coinsDropRadius;
        public float CoinsDropTime => coinsDropTime;

        [Header("Dash")]
        [SerializeField]
        private float dashDetectTime = 0.2f;
        [SerializeField]
        private float dashDistance = 1.0f;
        [SerializeField]
        private float dashTime = 0.25f;
        [SerializeField]
        private float dashCooldown = 1.0f;


        [Header("Collision with obstacle")]
        [SerializeField]
        private float pushForce = 3.0f;
        [SerializeField]
        private float pushTime = 0.1f;
        [SerializeField]
        private float invunerabilityDuration = 2.0f;
        [SerializeField]
        private float invunerabilityBlinkInterval = 0.25f;
        [SerializeField]
        private CoinComponent coinPrefab;
        [MinMaxSlider(0, 50)]
        [SerializeField]
        private Vector2Int coinsDropCount;
        [SerializeField]
        private float coinsDropRadius = 1.0f;
        [SerializeField]
        private float coinsDropTime = 0.1f;
    }
}
