using System;
using UnityEngine;

namespace SkyCrush.WSGenerator
{
    [Serializable]
    public struct AreaInfo
    {
        public Vector2 center;
        public Vector2 size;
        public Color color;
        public bool drawGizmos;
    }
}
