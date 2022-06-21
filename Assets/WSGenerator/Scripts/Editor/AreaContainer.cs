using System;
using UnityEngine;
using NaughtyAttributes;

namespace SkyCrush.WSGenerator
{
    [CreateAssetMenu(fileName = "WSG_AreaContainer", menuName = "WSGenerator/AreaContainer", order = 2)]
    public class AreaContainer : ScriptableObject
    {
        [Header("Area Settings")]
        [ReorderableList]
        public AreaInfo[] areas;
    }

    [Serializable]
    public struct AreaInfo
    {
        public Vector2 point1;
        public Vector2 point2;
        public Color color;
        public bool drawGizmos;
    }
}