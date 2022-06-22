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
        private readonly EcsWorld _ecsWorld;
        private readonly GameConfiguration _gameConfigs;
        private readonly Generator _generator;

        private readonly EcsFilter<EcsGameObject, MoveWorldObject> _filter1;
        private readonly EcsFilter<EcsGameObject, ActiveObject> _filter2;
        private readonly EcsFilter<EcsGameObject, InactiveObject> _filter3;

        private Transform _moveWorldObject;

        public void Init()
        {
            _moveWorldObject = _filter1.Get1(0).instance.transform;

            _generator.PoolManager.OnTakeFromPools += ActivateCallback;
            _generator.PoolManager.OnReturnToPools += DeactivateCallback;
        }

        public void Run()
        {
            foreach(var i in _filter2)
            {
                ref var gameObject = ref _filter2.Get1(i);

                if (gameObject.instance == null || gameObject.instance.transform.position.y <= _gameConfigs.MaxActiveObjectPosition) continue;

                _filter2.GetEntity(i).Del<ActiveObject>();
                _filter2.GetEntity(i).Replace(new InactiveObject());
            }

            foreach (var i in _filter3)
            {
                var gameObject = _filter3.Get1(i);

                _filter3.GetEntity(i).Del<InactiveObject>();
                _generator.PoolManager.GetPoolContainer(gameObject.instance.name, true).Release(gameObject.instance);
            }
        }

        public void Destroy()
        {
            _generator.PoolManager.OnTakeFromPools -= ActivateCallback;
            _generator.PoolManager.OnReturnToPools -= DeactivateCallback;
        }

        private void ActivateCallback(GameObject poolObject)
        {
            if(poolObject.TryGetComponent(out ConvertToEntity convertToEntity))
            {
                var entity = convertToEntity.TryGetEntity();
                if(entity != null && entity.HasValue)
                {
                    var oldEntity = entity.Value;
                    convertToEntity.Set(AddNeededComponentsToEntity(ref oldEntity, poolObject));
                }
                else
                {
                    var newEntity = _ecsWorld.NewEntity();
                    newEntity = AddNeededComponentsToEntity(ref newEntity, poolObject);
                }
            }
            else
            {
                var newEntity = _ecsWorld.NewEntity();
                newEntity = AddNeededComponentsToEntity(ref newEntity, poolObject);
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
