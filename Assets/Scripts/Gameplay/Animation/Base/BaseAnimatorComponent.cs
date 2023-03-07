using System;
using System.Collections.Generic;
using UnityEngine;
using Voody.UniLeo;
using SkyCrush.Utility;

namespace RiderGame.Gameplay
{
    public class BaseAnimatorComponent : MonoProvider<BaseAnimatorController> { }

    [Serializable]
    public struct BaseAnimatorController
    {
        public class AnimationInfo
        {
            public int priority;
            public SpriteAnimation animation;
            public bool loop;
            public bool continueFrame;
            public int flipDir;
            public Action onEndPlay;
        }

        public SpriteAnimator spriteAnimator;

        public List<AnimationInfo> animationsInfo;
        public AnimationInfo currentAnimationInfo;
    }
}
