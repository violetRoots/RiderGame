using System;
using UnityEngine;
using Voody.UniLeo;
using RiderGame.Inputs;

namespace RiderGame.Player
{
    public class PlayerMovementComponent : MonoProvider<PlayerMovement> { }

    [Serializable]
    public struct PlayerMovement
    {
        public Rigidbody2D rigidbody;
        public float speed;
    }
}