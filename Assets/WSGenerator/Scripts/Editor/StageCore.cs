using System;
using UnityEngine;

namespace SkyCrush.WSGenerator
{
    public partial class Stage
    {
        public void UpdateAreas(AreaContainer areaContainer)
        {
            foreach (var objectInfo in generateObjects) objectInfo.UpdateAreaValue(areaContainer);
        }

        public void UpdatePool(ref PoolInfo[] poolsInfo)
        {
            foreach (var objectInfo in generateObjects) objectInfo.UpdatePool(ref poolsInfo);
        }
    }
}
