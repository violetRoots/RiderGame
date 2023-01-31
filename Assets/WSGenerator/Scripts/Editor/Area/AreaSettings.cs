using System;
using UnityEngine;
using NaughtyAttributes;

namespace SkyCrush.WSGenerator
{
    [CreateAssetMenu(fileName = "WSG_AreaSettings", menuName = "WSGenerator/AreaSettings", order = 3)]
    public partial class AreaSettings : ScriptableObject
    {
        [Header("Area Settings")]
        [SerializeField]
        private Area2D areaPrefab;

        [ReorderableList]
        [SerializeField]
        private AreaInfo[] areas;
    }
}
