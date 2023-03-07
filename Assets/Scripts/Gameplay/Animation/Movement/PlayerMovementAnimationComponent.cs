using System;
using UnityEngine;
using Voody.UniLeo;
using RiderGame.SO;

namespace RiderGame.Gameplay
{
    public sealed class PlayerMovementAnimationComponent : MonoProvider<MovementAnimation>
    {
        public MovementAnimation Value => value;
    }

    [Serializable]
    public struct MovementAnimation
    {
        public bool drawGizmos;

        [HideInInspector]
        public SpriteAnimationConfiguration animationConfiguration;
    }
}