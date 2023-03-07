using UnityEngine;
using RiderGame.SO;
using System;

namespace RiderGame.Gameplay
{
    public class State : ContainerElement
    {
        public float DirectionAngle
        {
            get => directionAngle;
            set => directionAngle = Mathf.Clamp(value, -GameConfiguration.ClampDirectionAngle, GameConfiguration.ClampDirectionAngle);
        }

        private float directionAngle;

        [HideInInspector]
        public IDisposable changeStateSubscription;
    }
}
