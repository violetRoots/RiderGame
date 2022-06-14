using UnityEngine;
using UnityEditor;
using NaughtyAttributes;

namespace SkyCrush.WSGenerator
{
    [CreateAssetMenu(fileName = "WSG_Settings", menuName = "WSGenerator/Settings", order = 0)]
    public class Settings : SingletonSOEditorOnly<Settings>
    {
        public AreaContainer AreaContainer => areaContainer;

        [Header("Area Settings")]
        [Expandable]
        [SerializeField]
        private AreaContainer areaContainer;

        public static void Select()
        {
            Selection.activeObject = Instance;
        }

        public static void SelectAreaContainer()
        {
            Selection.activeObject = Instance.AreaContainer;
        }
    }
}
