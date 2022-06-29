using UnityEngine;
using NaughtyAttributes;

namespace SkyCrush.WSGenerator
{
    [CreateAssetMenu(fileName = "WSG_PoolSettings", menuName = "WSGenerator/PoolSettings", order = 3)]
    public partial class PoolSettings : ScriptableObject
    {
        [ReorderableList]
        [SerializeField]
        private PoolObjectInfo[] poolsInfo;
    }
}
