using UnityEngine;

namespace RiderGame.Gameplay
{
    public interface IMovableState
    {
        public float MovementSpeed { get; set; }
        public float DirectionAngle { get; set; }

        public void SetClampDirectionAngle(float value);
    }
}
