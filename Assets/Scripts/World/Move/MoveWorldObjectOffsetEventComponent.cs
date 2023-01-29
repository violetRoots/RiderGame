using UnityEngine;
using DG.Tweening;
using Voody.UniLeo;

namespace RiderGame.World
{
    public class MoveWorldObjectOffsetEventComponent : MonoProvider<MoveWorldObjectOffsetEvent> { }

    public struct MoveWorldObjectOffsetEvent
    {
        public Vector3 offset;
        public float time;
        public Ease ease;
        public bool stopMovement;
    }
}
