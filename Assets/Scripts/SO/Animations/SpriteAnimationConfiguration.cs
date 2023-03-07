using System;
using UnityEngine;
using NaughtyAttributes;
using SkyCrush.Utility;

namespace RiderGame.SO
{
    [CreateAssetMenu(fileName = "SpriteAnimationConfigs", menuName = "RiderGame/AnimationConfiguration", order = 4)]
    public class SpriteAnimationConfiguration : ScriptableObject
    {
        [Serializable]
        public struct AnimationConfigurationInfo
        {
            public int FlipValue => isFlip ? -1 : 1;

            public float angle;
            public SpriteAnimation animation;
            public bool isFlip;
        }

        [Header("Animation")]
        [ReorderableList]
        public AnimationConfigurationInfo[] animationsInfo;

        public AnimationConfigurationInfo GetAnimationByAngle(float angle)
        {
            AnimationConfigurationInfo res = default;
            var minDifference = float.MaxValue;
            foreach (var animationInfo in animationsInfo)
            {
                var difference = Mathf.Abs(angle - animationInfo.angle);

                if (difference < minDifference)
                {
                    minDifference = difference;
                    res = animationInfo;
                }
            }

            return res;
        }
    }
}
