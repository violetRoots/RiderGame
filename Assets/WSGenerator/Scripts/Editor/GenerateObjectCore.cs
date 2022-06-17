using System;
using UnityEngine;
using NaughtyAttributes;

namespace SkyCrush.WSGenerator
{
    public partial class GenerateObject
    {
        private const string InstanceDropdownName = nameof(_instances);
        private const string AreaDropdownName = nameof(_areaIndexes);

        private GameObject[] _instances = new GameObject[1] { null };
        private PoolInfo[] _poolsInfo;

        private int[] _areaIndexes;
        private AreaInfo[] _areaValues;

        public void UpdateAreaValue(AreaContainer areaContainer)
        {
            _areaValues = areaContainer.areas;

            _areaIndexes = new int[_areaValues.Length];
            for (var i = 0; i < _areaValues.Length; i++) _areaIndexes[i] = i;

            areaIndex = Mathf.Clamp(areaIndex, 0, _areaIndexes.Length - 1);
            areaValue = _areaValues[areaIndex];
        }


        public void UpdatePool(ref PoolInfo[] poolsInfo)
        {
            _poolsInfo = poolsInfo;
            _instances = new GameObject[_poolsInfo.Length];

            for (var i = 0; i < _poolsInfo.Length; i++)
            {
                _instances[i] = _poolsInfo[i].instance;
            }
        }
    }
}
