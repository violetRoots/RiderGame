using System;
using UnityEngine;

namespace SkyCrush.WSGenerator
{
    public partial class Stage
    {
        public void UpdateAreas(AreaContainer areaContainer)
        {
            foreach (var objectInfo in objectInfo) objectInfo.UpdateAreaValue(areaContainer);
        }

        public void UpdatePool(ref GameObject[] poolObjects)
        {
            foreach (var objectInfo in objectInfo) objectInfo.UpdatePool(ref poolObjects);
        }
    }
}
