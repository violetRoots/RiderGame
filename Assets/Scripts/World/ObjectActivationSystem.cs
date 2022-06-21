using UnityEngine;
using Leopotam.Ecs;
using SkyCrush.WSGenerator;
using Voody.UniLeo;

namespace RiderGame.World
{
    public class ObjectActivationSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private readonly Generator _generator;

        private readonly EcsFilter<MoveWorldObject> _filter1;
        private readonly EcsFilter<ActiveObject> _filter2;

        private Transform _worldObject;

        public void Init()
        {
            _worldObject = _filter1.Get1(0).transform;

            _generator.PoolManager.OnTakeFromPools += ActivateObject;
            _generator.PoolManager.OnReturnToPools += DeactivateObject;
        }

        public void Run()
        {
            var count = 0;

            foreach(var i in _filter2)
            {
                count++;
            }

            Debug.Log(count);
        }

        public void Destroy()
        {
            _generator.PoolManager.OnTakeFromPools -= ActivateObject;
            _generator.PoolManager.OnReturnToPools -= DeactivateObject;
        }

        private void ActivateObject(GameObject poolObject)
        {
            poolObject.AddComponent<ActiveObjectComponent>();

            poolObject.transform.SetParent(_worldObject);
        }

        private void DeactivateObject(GameObject poolObject)
        {
            GameObject.Destroy(poolObject.GetComponent<ActiveObjectComponent>());

            poolObject.transform.SetParent(_generator.PoolManager.GetPoolContainer(poolObject).Container);
        }
    }
}
