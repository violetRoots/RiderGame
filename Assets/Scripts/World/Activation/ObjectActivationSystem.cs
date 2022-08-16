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
        public static readonly List<EcsEntity> ActiveObjectEntities = new List<EcsEntity>();
        private static readonly List<GameObject> ObjectsToActivate = new List<GameObject>();

        private bool IsEventHandling { get; set; } = true;

        private readonly GameConfiguration _gameConfigs;
        private readonly Generator _generator;
        private readonly EcsStartup _ecsStartupObject;

        private readonly EcsFilter<EcsGameObject> _fGameObjects;
        private readonly EcsFilter<EcsGameObject, MoveWorldObject> _fWorldObject;
        private readonly EcsFilter<EcsGameObject, ActiveObject> _fActiveObject;
        private readonly EcsFilter<EcsGameObject, DeactivationEvent> _fDeactivationEvent;

        private Transform _worldObject;

        public static GameObject Instantiate(GameObject gameObject, Vector2 position)
        {
            var instantiatedObject = GameObject.Instantiate(gameObject, position, Quaternion.identity);
            ObjectsToActivate.Add(instantiatedObject);
            return instantiatedObject;
        }

        public void Init()
        {
            _worldObject = _fWorldObject.Get1(0).instance.transform;

            _generator.PoolManager.OnTakeFromPools += ActivateCallbak;
            _generator.PoolManager.OnReturnToPools += Deactivate;

            _ecsStartupObject.StartCoroutine(EventsProcessingOnEndOfFrame());
        }

        public void Run()
        {
            ActiveObjectEntities.Clear();
            foreach(var i in _fActiveObject)
            {
                ref var entity = ref _fActiveObject.GetEntity(i);
                ref var gameObject = ref _fActiveObject.Get1(i);

                ActiveObjectEntities.Add(entity);

                if (gameObject.instance == null || gameObject.instance.transform.position.y <= _gameConfigs.MaxActiveObjectPosition) continue;

                entity.Del<ActiveObject>();
                entity.Replace(new DeactivationEvent());
                entity.Replace(new InactiveObject());
            }
        }

        public void Destroy()
        {
            _generator.PoolManager.OnTakeFromPools -= ActivateCallbak;
            _generator.PoolManager.OnReturnToPools -= Deactivate;
        }

        private void ActivateCallbak(GameObject poolObject)
        {
            ObjectsToActivate.Add(poolObject);
        }

        private IEnumerator EventsProcessingOnEndOfFrame()
        {
            while (IsEventHandling)
            {
                yield return new WaitForEndOfFrame();

                ActivateOnEndOfFrame();
                DeactivateOnEndOfFrame();
            }
        }

        private void ActivateOnEndOfFrame()
        {
            foreach (var i in _fGameObjects)
            {
                var gameObject = _fGameObjects.Get1(i);

                var entity = _fGameObjects.GetEntity(i);

                if (!ObjectsToActivate.Contains(gameObject.instance))
                {
                    if (entity.Has<ActivationEvent>()) entity.Del<ActivationEvent>();
                }
                else
                {
                    if (entity.Has<InactiveObject>()) entity.Del<InactiveObject>();

                    entity.Replace(new ActivationEvent());
                    entity.Replace(new ActiveObject());

                    gameObject.instance.transform.SetParent(_worldObject);

                    ObjectsToActivate.Remove(gameObject.instance);
                }
            }
        }

        private void DeactivateOnEndOfFrame()
        {
            foreach (var i in _fDeactivationEvent)
            {
                var gameObject = _fDeactivationEvent.Get1(i);

                _generator.PoolManager.GetPoolContainer(gameObject.instance.name, true).Release(gameObject.instance);
                _fDeactivationEvent.GetEntity(i).Del<DeactivationEvent>();
            }
        }

        private void Deactivate(GameObject poolObject)
        {
            poolObject.transform.SetParent(_generator.PoolManager.GetPoolContainer(poolObject.name, true).Container);
        }
    }
}
