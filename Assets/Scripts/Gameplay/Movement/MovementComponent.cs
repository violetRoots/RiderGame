using UnityEngine;
using Voody.UniLeo;
using RiderGame.SO;

namespace RiderGame
{
    public class MovementComponent : MonoProvider<Movement> { }

    public struct Movement
    {
        public float DirectionAngle
        {
            get => directionAngle;
            set => directionAngle = Mathf.Clamp(value, -GameConfiguration.Instance.ClampDirectionAngle, GameConfiguration.Instance.ClampDirectionAngle);
        }

        private float directionAngle;
    }
}
