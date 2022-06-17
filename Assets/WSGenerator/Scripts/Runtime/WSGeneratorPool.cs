using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyCrush.WSGenerator
{
    public partial class WSGenerator
    {
        private const string PoolsContainersParentName = "Pools";

        public List<PoolContainer> PoolContainers { get; private set; } = new List<PoolContainer>();

        private void InitPools()
        {
            var poolsContainersParent = new GameObject(PoolsContainersParentName);
            poolsContainersParent.transform.SetParent(transform);

            foreach(var poolInfo in sequence.PoolsInfo)
            {
                PoolContainers.Add(new PoolContainer(poolInfo, poolsContainersParent.transform));
            }
        }

        public void ClearPools()
        {
            foreach (var poolContainer in PoolContainers)
            {
                Destroy(poolContainer.Container.gameObject);

                poolContainer.Pool.Dispose();
                poolContainer.Pool.Clear();
            }

            PoolContainers.Clear();
        }
    }
}
