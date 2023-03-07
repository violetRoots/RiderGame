using RiderGame.SO;
using UnityEngine;
using Voody.UniLeo;

namespace RiderGame.Gameplay
{
    public class PlayerMovementComponent : MonoProvider<PlayerMovement> { }

    public struct PlayerMovement
    {
        public float DirectionAngle
        {
            get => directionAngle;
            set => directionAngle = Mathf.Clamp(value, -GameConfiguration.ClampDirectionAngle, GameConfiguration.ClampDirectionAngle);
        }

        private float directionAngle;
    }
}
