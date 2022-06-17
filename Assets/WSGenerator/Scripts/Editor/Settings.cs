using UnityEngine;
using UnityEditor;
using NaughtyAttributes;

namespace SkyCrush.WSGenerator
{
    [CreateAssetMenu(fileName = "WSG_Settings", menuName = "WSGenerator/Settings", order = 0)]
    public partial class Settings : SingletonSOEditorOnly<Settings>
    {
        [Header("Area Settings")]
        [Expandable]
        [SerializeField]
        private AreaContainer areaContainer;

        //[Header("Pool Settings")]
        //[Expandable]
        //[SerializeField]
        //private AreaContainer areaContainer;
    }
}
