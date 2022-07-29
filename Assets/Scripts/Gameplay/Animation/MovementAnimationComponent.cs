using System;
using UnityEngine;
using Voody.UniLeo;
using NaughtyAttributes;
using RiderGame.SO;
using SkyCrush.Utility;

namespace RiderGame.Gameplay
{
    public sealed class MovementAnimationComponent : MonoProvider<MovementAnimation>
    {
        public MovementAnimation Value => value;
    }

    [Serializable]
    public struct MovementAnimation
    {
        public bool setCustomAnimations;

        public SpriteAnimator spriteAnimator;
        public bool drawGizmos;

        [Space(10)]
        [AllowNesting]
        [ShowIf(nameof(setCustomAnimations))]
        public SpriteAnimationConfiguration animationConfiguration;

        [HideInInspector]
        public float directionAngle;
    }
}