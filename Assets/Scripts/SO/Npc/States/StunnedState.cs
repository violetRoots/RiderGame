using System;
using UnityEngine;

namespace RiderGame.Gameplay
{
    [Serializable]
    public class StunnedState : State
    {
        public float movementSpeed = 3.0f;
    }

    [Serializable]
    public struct StunnedStateInfo
    {
        public SpriteRenderer icon;
    }
}
