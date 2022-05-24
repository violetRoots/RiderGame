using System;
using UnityEngine;
using Voody.UniLeo;

namespace RiderGame.World
{
    public class WorldObjectComponent : MonoProvider<WorldObject> { }

    [Serializable]
    public struct WorldObject
    {
        public Rigidbody2D rigidbody;
    }
}