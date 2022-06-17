using System;
using UnityEngine;
using UnityEngine.Pool;

namespace SkyCrush.WSGenerator
{
    public class PoolContainer
    {
        private int MaxPoolSize = 50;

        public GameObject Instance { get; private set; }
        public Transform Container { get; private set; }
        public bool CollectionCheck { get; private set; }

        public Action<GameObject> OnReturnToPool;
        public Action<GameObject> OnTakeFromPool;

        public ObjectPool<GameObject> Pool => _pool;

        private ObjectPool<GameObject> _pool;

        public PoolContainer(PoolInfo poolInfo, Transform parent)
        {
            Instance = poolInfo.instance;

            var poolObject = new GameObject($"Pool Container [{Instance.name}]");
            Container = poolObject.transform;
            Container.SetParent(parent);

            for (var i = 1; i <= poolInfo.count; i++)
            {
                _pool = new ObjectPool<GameObject>(CreatePoolInstance, OnReturnToPool, OnTakeFromPool, OnDestroyPool, CollectionCheck, poolInfo.count, MaxPoolSize);
            }
        }

        private GameObject CreatePoolInstance()
        {
            var poolObject = GameObject.Instantiate(Instance, Container);
            poolObject.SetActive(false);
            return poolObject;
        }

        private void ReleaseCallback(GameObject poolObject)
        {
            poolObject.SetActive(false);
            OnReturnToPool?.Invoke(poolObject);
        }

        private void GetCallback(GameObject poolObject)
        {
            poolObject.SetActive(true);
            OnTakeFromPool?.Invoke(poolObject);
        }

        private void OnDestroyPool(GameObject poolObject)
        {
            GameObject.Destroy(poolObject);
        }
    }
}
