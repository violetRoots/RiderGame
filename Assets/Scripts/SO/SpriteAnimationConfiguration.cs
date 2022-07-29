using System;
using UnityEngine;
using NaughtyAttributes;
using SkyCrush.Utility;

namespace RiderGame.SO
{
    [CreateAssetMenu(fileName = "SpriteAnimationConfigs", menuName = "RiderGame/AnimationConfiguration", order = 4)]
    public class SpriteAnimationConfiguration : ScriptableObject
    {
        public AnimationInfo[] AnimationsInfo => animationsInfo;

        [Header("Animation")]
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
