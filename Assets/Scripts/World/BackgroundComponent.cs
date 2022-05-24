using System;
using UnityEngine;
using Voody.UniLeo;

namespace RiderGame.World
{
    public class BackgroundComponent : MonoProvider<Background> { }

    [Serializable]
    public struct Background 
    {
        public SpriteRenderer renderer;
    }
}


