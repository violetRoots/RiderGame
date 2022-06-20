using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkyCrush.WSGenerator
{
    public class PoolManager
    {
        private const string PoolsContainersParentName = "Pools";

        private List<PoolContainer> _poolContainers = new List<PoolContainer>();

        public void Init(Sequence sequence, Transform transform)
        {
            var poolsContainersParent = new GameObject(PoolsContainersParentName);
            poolsContainersParent.transform.SetParent(transform);

            foreach(var poolInfo in sequence.PoolsInfo)
            {
                _poolContainers.Add(new PoolContainer(poolInfo, poolsContainersParent.transform));
            }
        }

        public PoolContainer GetPoolContainer(GameObject instance)
        {
            PoolContainer res = null;

            foreach(var poolContainer in _poolContainers)
            {
                if (poolContainer.Instance != instance) continue;

                res = poolContainer;
            }

            return res;
        }

        public void Clear()
        {
            foreach (var poolContainer in _poolContainers)
            {
                GameObject.Destroy(poolContainer.Container.gameObject);

                poolContainer.CLear();
            }

            _poolContainers.Clear();
        }
    }
}
