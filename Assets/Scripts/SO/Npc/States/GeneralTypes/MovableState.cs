using RiderGame.SO;
using SkyCrush.Utility;
using System;
using UnityEngine;

namespace RiderGame.Gameplay
{
    [Serializable]
    public class MovableState : State
    {
        public SpriteAnimationConfiguration MovementAnimation => movementAnimation;
        public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }

        public float DirectionAngle
        {
            get => directionAngle;
            set => directionAngle = CustomMath.ClampAngle(value, -GameConfiguration.ClampDirectionAngle, GameConfiguration.ClampDirectionAngle);
        }

        [SerializeField]
        private SpriteAnimationConfiguration movementAnimation;

        [SerializeField]
        private float movementSpeed = 3.0f;

        private float directionAngle;
    }
}