using System;
using UnityEngine;
using Voody.UniLeo;

namespace RiderGame.World
{
    public class MoveWorldObjectComponent : MonoProvider<MoveWorldObject> { }

    [Serializable]
    public struct MoveWorldObject { }
}