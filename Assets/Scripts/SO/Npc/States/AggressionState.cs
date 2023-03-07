using System;
using UnityEngine;

namespace RiderGame.Gameplay
{
    [Serializable]
    public class AggressionState : MovableState
    {
        public float aggressionRadius = 3.0f;
    }

    [Serializable]
    public struct AggressionStateRefs
    {
        public SpriteRenderer icon;
    }
}
