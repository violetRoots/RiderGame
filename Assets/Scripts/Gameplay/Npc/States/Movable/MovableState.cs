using RiderGame.SO;
using System;
using UnityEngine;

namespace RiderGame.Gameplay
{
    [Serializable]
    public class MovableState : State
    {
        public SpriteAnimationConfiguration MovementAnimation => movementAnimation;
        public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }

        [SerializeField]
        private SpriteAnimationConfiguration movementAnimation;

        [SerializeField]
        private float movementSpeed = 3.0f;
    }
}
