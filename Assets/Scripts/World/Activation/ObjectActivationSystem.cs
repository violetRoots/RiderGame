using UnityEngine;
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
        private static readonly List<ObjectActivationInfo> ObjectsToActivate = new List<ObjectActivationInfo>();

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
            ActivateCallback(instantiatedObject);
            return instantiatedObject;
        }

        public void Init()
        {
            _worldObject = _fWorldObject.Get1(0).instance.transform;

            _generator.PoolManager.OnTakeFromPools += ActivateCallback;
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
                entity.Replace(new DeactivationEvent());
                entity.Replace(new InactiveObject());
            }
        }

        public void Destroy()
        {
            _generator.PoolManager.OnTakeFromPools -= ActivateCallback;
            _generator.PoolManager.OnReturnToPools -= Deactivate;
        }

        private static void ActivateCallback(GameObject poolObject)
        {

            ObjectsToActivate.Add(new ObjectActivationInfo(poolObject, false));

            var nestedObjectsToActivate = poolObject.GetComponentsInChildren<ConvertToEntity>();
            foreach(var nested in nestedObjectsToActivate)
            {
                if (nested.gameObject == poolObject) continue;

                ObjectsToActivate.Add(new ObjectActivationInfo(nested.gameObject, true));
            }
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

                var info = ObjectsToActivate.Find((info) => info.Instance == gameObject.instance);

                if (info == null)
                {
                    if (entity.Has<ActivationEvent>()) entity.Del<ActivationEvent>();
                }
                else
                {
                    if (entity.Has<InactiveObject>()) entity.Del<InactiveObject>();

                    var activeObject = new ActiveObject() { isNested = info.IsNested };

                    if(!activeObject.isNested) gameObject.instance.transform.SetParent(_worldObject);

                    entity.Replace(new ActivationEvent());
                    entity.Replace(activeObject);

                    ObjectsToActivate.Remove(info);
                }
            }
        }

        private void DeactivateOnEndOfFrame()
        {
            foreach (var i in _fDeactivationEvent)
            {
                var entity = _fDeactivationEvent.GetEntity(i);
                var gameObject = _fDeactivationEvent.Get1(i);
                
                if(entity.Has<ActiveObject>() && entity.Get<ActiveObject>().isNested == false)
                {
                    _generator.PoolManager.GetPoolContainer(gameObject.instance.name, true).Release(gameObject.instance);
                }

                if (entity.Has<ActiveObject>()) entity.Del<ActiveObject>();
                entity.Del<DeactivationEvent>();
            }
        }

        private void Deactivate(GameObject poolObject)
        {
            poolObject.transform.SetParent(_generator.PoolManager.GetPoolContainer(poolObject.name, true).Container);
        }
    }

    public class ObjectActivationInfo
    {
        public GameObject Instance { get; private set; }
        public bool IsNested { get; private set; }

        public ObjectActivationInfo(GameObject instance, bool isNested)
        {
            Instance = instance;
            IsNested = isNested;
        }
    }
}
