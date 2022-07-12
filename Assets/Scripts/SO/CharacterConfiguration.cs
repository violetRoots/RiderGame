using System;
using UnityEngine;
using NaughtyAttributes;
using SkyCrush.Utility;

namespace RiderGame.SO
{
    [CreateAssetMenu(fileName = "Character_1", menuName = "RiderGame/Character", order = 2)]
    public class CharacterConfiguration : ScriptableObject
    {
        public AnimationInfo[] AnimationsInfo => animationsInfo;

        [ReorderableList]
        [SerializeField]
        private AnimationInfo[] animationsInfo;
    }

    [Serializable]
    public struct AnimationInfo
    {
        public float angle;
        public SpriteAnimation animation;
        public bool isFlip;
    }
}
