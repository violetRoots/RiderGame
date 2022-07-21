using System;
using UnityEngine;
using Voody.UniLeo;
using RiderGame.SO;
using SkyCrush.Utility;

namespace RiderGame.Gameplay
{
    public sealed class CharacterAnimationComponent : MonoProvider<CharacterAnimation> { }

    [Serializable]
    public struct CharacterAnimation
    {
        public SpriteAnimator spriteAnimator;
    }
}