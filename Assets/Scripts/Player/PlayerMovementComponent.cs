using System;
using UnityEngine;
using Voody.UniLeo;

namespace RiderGame.Player
{
    public class PlayerMovementComponent : MonoProvider<PlayerMovement> { }

    [Serializable]
    public struct PlayerMovement
    {
        public Rigidbody2D rigidbody;
    }
}