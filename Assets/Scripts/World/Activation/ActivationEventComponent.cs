using UnityEngine;
using Voody.UniLeo;

namespace RiderGame.World
{
    public class ActivationEventComponent : MonoProvider<ActivationEvent> { }

    public struct ActivationEvent
    {
        public int aliveFrames;
    }
}
