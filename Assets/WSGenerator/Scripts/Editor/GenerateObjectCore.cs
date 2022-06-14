using System;
using UnityEngine;
using NaughtyAttributes;

namespace SkyCrush.WSGenerator
{
    public partial class GenerateObject
    {
        private const string instanceDropdownName = nameof(poolObjects);
        private const string areaDropdownName = nameof(areaIndexes);

        private GameObject[] poolObjects;

        private int[] areaIndexes;
        private AreaInfo[] areaValues;

        public void UpdateAreaValue(AreaContainer areaContainer)
        {
            areaValues = areaContainer.areas;

            areaIndexes = new int[areaValues.Length];
            for (var i = 0; i < areaValues.Length; i++) areaIndexes[i] = i;

            areaIndex = Mathf.Clamp(areaIndex, 0, areaIndexes.Length - 1);
            areaValue = areaValues[areaIndex];
        }


        public void UpdatePool(ref GameObject[] poolObjects)
        {
            this.poolObjects = poolObjects;
        }
    }
}
