using System;
using UnityEngine;

namespace RiderGame.Gameplay
{
    [Serializable]
    public class StunnedState : State
    {
        public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }

        [SerializeField]
        private float movementSpeed = 3.0f;
    }

    [Serializable]
    public struct StunnedStateInfo
    {
        public SpriteRenderer icon;
    }
}
