using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RiderGame.Gameplay
{
    [SerializeField]
    public class WalkState : State
    {
        [SerializeField]
        public float movementSpeed = 3.0f;
    }
}
