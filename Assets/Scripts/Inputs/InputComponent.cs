using System;
using UnityEngine;
using Voody.UniLeo;

namespace RiderGame.Inputs
{
    public sealed class InputComponent : MonoProvider<Input> { }

    public struct Input
    {
        public struct TapInfo
        {
            public bool started;
            public float startedTime;
            public bool ended;
            public float endedTime;
        }

        public TapInfo tap;
        public bool swipeDown;
        public Vector2 mouseDelta;
    }
}