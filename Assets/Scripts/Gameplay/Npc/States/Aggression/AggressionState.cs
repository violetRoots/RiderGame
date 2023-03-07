using System;
using UnityEngine;

namespace RiderGame.Gameplay
{
    [Serializable]
    public class AggressionState : MovableState
    {
        public float startAggressionRadius = 3.0f;
        public float endAggressionRadius = 10.0f;
    }

    [Serializable]
    public struct AggressionStateRefs
    {
        public SpriteRenderer icon;
    }
}
