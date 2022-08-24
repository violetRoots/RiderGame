using UnityEngine;

namespace RiderGame.SO
{
    [CreateAssetMenu(fileName = "Character_1", menuName = "RiderGame/Character", order = 3)]
    public class CharacterConfiguration : ScriptableObject
    {
        public SpriteAnimationConfiguration AnimationConfiguration => animationConfiguration;

        [SerializeField]
        private SpriteAnimationConfiguration animationConfiguration;
    }
}
