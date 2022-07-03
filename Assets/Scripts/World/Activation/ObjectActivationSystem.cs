using UnityEngine;
using Leopotam.Ecs;
using SkyCrush.WSGenerator;
using RiderGame.SO;
using System.Collections;
using System.Collections.Generic;
using Voody.UniLeo;

namespace RiderGame.World
{
    public class ObjectActivationSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private readonly GameConfiguration _gameConfigs;
        private readonly Generator _generator;
        private readonly EcsStartup _ecsStartupObject;


        private readonly EcsFilter<EcsGameObject> _filter1;
        private readonly EcsFilter<EcsGameObject, MoveWorldObject> _filter2;
        private readonly EcsFilter<EcsGameObject, ActiveObject> _filter3;
        private readonly EcsFilter<EcsGameObject, InactiveObject> _filter4;

        private Transform _moveWorldObject;

        public void Init()
        {
            _moveWorldObject = _filter2.Get1(0).instance.transform;

            _generator.PoolManager.OnTakeFromPools += ActivateCallback;
            _generator.PoolManager.OnReturnToPools += DeactivateCallback;
        }

        public void Run()
        {
            foreach(var i in _filter3)
            {
                ref var gameObject = ref _filter3.Get1(i);

                if (gameObject.instance == null || gameObject.instance.transform.position.y <= _gameConfigs.MaxActiveObjectPosition) continue;

                _filter3.GetEntity(i).Del<ActiveObject>();
                _filter3.GetEntity(i).Replace(new InactiveObject());
            }

            _ecsStartupObject.StartCoroutine(DeactivateOnEndOfFrame());
        }

        public void Destroy()
        {
            _generator.PoolManager.OnTakeFromPools -= ActivateCallback;
            _generator.PoolManager.OnReturnToPools -= DeactivateCallback;
        }

        private IEnumerator DeactivateOnEndOfFrame()
        {
            yield return new WaitForEndOfFrame();

            foreach (var i in _filter4)
            {
                var gameObject = _filter4.Get1(i);

                _filter4.GetEntity(i).Del<InactiveObject>();
                _generator.PoolManager.GetPoolContainer(gameObject.instance.name, true).Release(gameObject.instance);
            }
        }

        private void ActivateCallback(GameObject poolObject)
        {
            _ecsStartupObject.StartCoroutine(ActivateProcess(poolObject));
        }

        private IEnumerator ActivateProcess(GameObject poolObject)
        {
            yield return null;

            foreach (var i in _filter1)
            {
                var gameObject = _filter1.Get1(i);

                if (gameObject.instance != poolObject) continue;

                var entity = _filter1.GetEntity(i);
                entity.Replace(new ActiveObject());
            }

            poolObject.transform.SetParent(_moveWorldObject);
        }

        private EcsEntity AddNeededComponentsToEntity(ref EcsEntity entity, GameObject poolObject)
        {
            var gameObject = new EcsGameObject();
            gameObject.instance = poolObject;

            entity.Replace(gameObject);
            entity.Replace(new ActiveObject());

            return entity;
        }

        private void DeactivateCallback(GameObject poolObject)
        {
            poolObject.transform.SetParent(_generator.PoolManager.GetPoolContainer(poolObject.name, true).Container);
        }
    }
}
