using UnityEngine;
using Voody.UniLeo;

namespace RiderGame.Gameplay
{
    public class MovementComponent : MonoProvider<Movement> { }

    public struct Movement
    {
        public float DirectionAngle
        {
            get => directionAngle;
            set => directionAngle = Mathf.Clamp(value, -60, 60);
        }

        public float clampAngle;

        private float directionAngle;
    }
}
