using System;
using UnityEngine;
using Voody.UniLeo;

namespace RiderGame.World
{
    public class OverlayComponent : MonoProvider<Overlay> 
    {
        public Overlay Value => value;
    }

    [Serializable]
    public struct Overlay
    {
        public SpriteRenderer spriteRenderer;
        public float layerEdgeOffset;
    }
}
