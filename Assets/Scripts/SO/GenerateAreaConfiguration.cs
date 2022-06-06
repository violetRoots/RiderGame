using System;
using UnityEditor;
using UnityEngine;
using NaughtyAttributes;

namespace RiderGame.SO
{
    [CreateAssetMenu(fileName = "GenerateAreaConfigs_1", menuName = "RiderGame/GenerateAreaConfigs")]
    public class GenerateAreaConfiguration : SingletonScriptableObject<GenerateAreaConfiguration>
    {
        public static void Select()
        {
            Selection.activeObject = GenerateAreaConfiguration.Instance;
        }

        [ReorderableList]
        public GenerateAreaInfo[] generateAreas;
    }

    [Serializable]
    public struct GenerateAreaInfo
    {
        public Vector2 point1;
        public Vector2 point2;
        public Color color;
        public bool drawGizmos;
    }
}
