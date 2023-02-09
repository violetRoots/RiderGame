using UnityEngine;

namespace RiderGame.SO
{
    [CreateAssetMenu(fileName = "Character_1", menuName = "RiderGame/Character", order = 3)]
    public class CharacterConfiguration : ScriptableObject
    {
        public SpriteAnimationConfiguration WalkAnimationConfigs => walkAnimationConfigs;
        public SpriteAnimationConfiguration DashAnimationConfigs => dashAnimationConfigs;

        public float DashDistance => dashDistance;
        public float DashDuration => dashDuration;
        public float DashCooldown => dashCooldown;

        [Header("Animation")]
        [SerializeField]
        private SpriteAnimationConfiguration walkAnimationConfigs;
        [SerializeField]
        private SpriteAnimationConfiguration dashAnimationConfigs;

        [Header("Dash")]
        [SerializeField]
        private float dashDistance = 1.0f;
        [SerializeField]
        private float dashDuration = 0.25f;
        [SerializeField]
        private float dashCooldown = 1.0f;
    }
}
