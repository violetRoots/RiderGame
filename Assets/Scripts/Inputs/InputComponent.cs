using System;
using UnityEngine;
using Voody.UniLeo;

namespace RiderGame.Inputs
{
    public sealed class InputComponent : MonoProvider<Input> { }

    public struct Input
    {
        public Vector2 mouseDelta;
    }
}