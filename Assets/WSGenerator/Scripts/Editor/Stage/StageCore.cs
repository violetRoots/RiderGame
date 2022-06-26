using System;
using UnityEngine;

namespace SkyCrush.WSGenerator
{
    public partial class Stage
    {
        public float Duration => duration;
        public CustomStageData CustomData => customData;
        public GenerateObject[] GenerateObjects => generateObjects;
        public void UpdateAreas()
        {
            foreach (var objectInfo in generateObjects) objectInfo.UpdateAreaValue();
        }

        public void UpdatePool(ref PoolInfo[] poolsInfo)
        {
            foreach (var objectInfo in generateObjects) objectInfo.UpdatePool(ref poolsInfo);
        }

        public void UpdateCurveDescription()
        {
            foreach (var objectInfo in generateObjects) objectInfo.UpdateCurveDescription(duration);
        }
    }
}
