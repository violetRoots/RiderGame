using UnityEngine;
using NaughtyAttributes;
using RiderGame.Gameplay;

namespace RiderGame.SO
{
    [CreateAssetMenu(fileName = "PlayerConfigs", menuName = "RiderGame/PlayerConfigs", order = 2)]
    public class PlayerConfiguration : ScriptableObject
    {
        public int MaxLifesCount => maxLifesCount;

        [Header("Lifes")]
        [SerializeField]
        private int maxLifesCount = 3;

        public float DashDetectTime => dashDetectTime;

        [Header("Mechanics: Dash")]
        [SerializeField]
        private float dashDetectTime = 0.2f;

        public float PushForce => pushForce;
        public float PushTime => pushTime;
        public float InvunerabilityDuration => invunerabilityDuration;
        public float InvunerabilityBlinkInterval => invunerabilityBlinkInterval;
        public CoinComponent CoinPrefab => coinPrefab;
        public Vector2Int CoinsDropCount => coinsDropCount;
        public float CoinsDropRadius => coinsDropRadius;
        public float CoinsDropTime => coinsDropTime;

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
