using System;
using Voody.UniLeo;

namespace RiderGame.World
{
    public class ActiveObjectTag : MonoProvider<ActiveObject> { }
    public struct ActiveObject
    {
        public bool isNested;
    }
}
