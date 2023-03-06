using UnityEngine;
using SkyCrush.Utility;

namespace RiderGame.Gameplay
{
    [SerializeField]
    public class WalkState : State, IMovableState
    {
        public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }

        public float DirectionAngle
        {
            get => directionAngle;
            set => directionAngle = CustomMath.ClampAngle(value, -clampAngle, clampAngle);
        }

        [SerializeField]
        private float movementSpeed = 3.0f;

        private float clampAngle;
        private float directionAngle;

        public void SetClampDirectionAngle(float value)
        {
            clampAngle = value;
        }
    }
}
