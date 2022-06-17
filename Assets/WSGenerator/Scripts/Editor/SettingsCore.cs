using UnityEngine;
using UnityEditor;
using NaughtyAttributes;

namespace SkyCrush.WSGenerator
{
    public partial class Settings
    {
        public AreaContainer AreaContainer => areaContainer;

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
