using System;
using UnityEngine;
using NaughtyAttributes;

namespace SkyCrush.WSGenerator
{
    [CreateAssetMenu(fileName = "WSG_AreaContainer", menuName = "WSGenerator/AreaContainer", order = 2)]
    public partial class AreaSettings : ScriptableObject
    {
        [Header("Area Settings")]
        [SerializeField]
        private Area2D areaPrefab;

        [ReorderableList]
        [SerializeField]
        private AreaInfo[] areas;
    }

    [Serializable]
    public struct AreaInfo
    {
        public Vector2 center;
        public Vector2 size;
        public Color color;
        public bool drawGizmos;
    }
}
