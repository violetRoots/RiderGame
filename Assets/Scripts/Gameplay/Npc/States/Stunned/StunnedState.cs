using System;
using UnityEngine;
using RiderGame.SO;

namespace RiderGame.Gameplay
{
    [Serializable]
    public class StunnedState : State
    {
        public SpriteAnimationConfiguration stunnedAnimation;
    }

    [Serializable]
    public struct StunnedStateInfo
    {
        public SpriteRenderer icon;
    }
}
