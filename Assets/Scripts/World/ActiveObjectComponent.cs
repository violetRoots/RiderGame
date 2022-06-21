using System;
using Voody.UniLeo;

namespace RiderGame.World
{
    public class ActiveObjectComponent : MonoProvider<ActiveObject> { }

    [Serializable]
    public struct ActiveObject { }
}
