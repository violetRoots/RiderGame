using System;
using UnityEngine;
using Voody.UniLeo;
using RiderGame.SO;
using SkyCrush.Utility;

namespace RiderGame.Gameplay
{
    public sealed class CharacterAnimationComponent : MonoProvider<CharacterAnimation>
    {
        public CharacterAnimation Value => value;
    }

    [Serializable]
    public struct CharacterAnimation
    {
        public CharacterConfiguration character;
        public SpriteAnimator spriteAnimator;
    }
}