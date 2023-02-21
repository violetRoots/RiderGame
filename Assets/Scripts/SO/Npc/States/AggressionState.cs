using System;
using UnityEngine;

namespace RiderGame.Gameplay
{
    [Serializable]
    public class AggressionState : State
    {
        [SerializeField]
        public float movementSpeed = 3.0f;
    }

    [Serializable]
    public struct AggressionStateInfo
    {
        public SpriteRenderer icon;
    }
}
