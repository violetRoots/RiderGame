using System;
using System.Collections.Generic;
using UnityEngine;

namespace SkyCrush.WSGenerator
{
    public class PoolManager
    {
        public event Action<GameObject> OnReturnToPools;
        public event Action<GameObject> OnTakeFromPools;

        private const string PoolsContainersParentName = "Pools";

        private PoolSettings _poolSettings;
        private List<PoolContainer> _poolContainers = new List<PoolContainer>();

        public void Init(Sequence sequence, Transform transform)
        {
            _poolSettings = Settings.Instance.PoolSettings;

            var poolsContainersParent = new GameObject(PoolsContainersParentName);
            poolsContainersParent.transform.SetParent(transform);

            foreach (var poolInfo in _poolSettings.PoolObjectsInfo)
            {
                var container = new PoolContainer(_poolSettings, poolInfo, poolsContainersParent.transform);

                container.OnTakeFromPool += GetCallback;
                container.OnReturnToPool += ReleaseCallback;

                _poolContainers.Add(container);
            }
        }

        public PoolContainer GetPoolContainer(string name, bool isFormat = false)
        {
            PoolContainer res = null;

            foreach(var poolContainer in _poolContainers)
            {
                var instanceName = poolContainer.Instance.name;

                if (isFormat)
                {
                    instanceName = string.Format("{0}(Clone)", instanceName);
                }

                if (name != instanceName) continue;

                res = poolContainer;
            }

            return res;
        }

        private void GetCallback(GameObject poolObject)
        {
            OnTakeFromPools?.Invoke(poolObject);
        }

        private void ReleaseCallback(GameObject poolObject)
        {
            OnReturnToPools?.Invoke(poolObject);
        }

        public void Clear()
        {
            foreach (var poolContainer in _poolContainers)
            {
                poolContainer.OnTakeFromPool -= GetCallback;
                poolContainer.OnReturnToPool -= ReleaseCallback;

                GameObject.Destroy(poolContainer.Container.gameObject);

                poolContainer.CLear();
            }

            _poolContainers.Clear();
        }
    }
}
