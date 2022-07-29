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
        private static readonly List<GameObject> ActivateObjects = new List<GameObject>();

        private readonly GameConfiguration _gameConfigs;
        private readonly Generator _generator;
        private readonly EcsStartup _ecsStartupObject;

        private readonly EcsFilter<EcsGameObject> _fGameObjects;
        private readonly EcsFilter<EcsGameObject, MoveWorldObject> _fWorldObject;
        private readonly EcsFilter<EcsGameObject, ActiveObject> _fActiveObject;
        private readonly EcsFilter<EcsGameObject, InactiveObject> _fInactiveObject;

        private Transform _worldObject;

        public static GameObject Instantiate(GameObject gameObject, Vector2 position)
        {
            var instantiatedObject = GameObject.Instantiate(gameObject, position, Quaternion.identity);
            ActivateObjects.Add(instantiatedObject);
            return instantiatedObject;
        }

        public void Init()
        {
            _worldObject = _fWorldObject.Get1(0).instance.transform;

            _generator.PoolManager.OnTakeFromPools += ActivateCallbak;
            _generator.PoolManager.OnReturnToPools += Deactivate;
        }

        public void Run()
        {
            foreach(var i in _fActiveObject)
            {
                ref var gameObject = ref _fActiveObject.Get1(i);

                if (gameObject.instance == null || gameObject.instance.transform.position.y <= _gameConfigs.MaxActiveObjectPosition) continue;

                _fActiveObject.GetEntity(i).Del<ActiveObject>();
                _fActiveObject.GetEntity(i).Replace(new InactiveObject());
            }

            _ecsStartupObject.StartCoroutine(ActivateOnEndOfFrame());
            _ecsStartupObject.StartCoroutine(DeactivateOnEndOfFrame());
        }

        public void Destroy()
        {
            _generator.PoolManager.OnTakeFromPools -= ActivateCallbak;
            _generator.PoolManager.OnReturnToPools -= Deactivate;
        }

        private void ActivateCallbak(GameObject poolObject)
        {
            ActivateObjects.Add(poolObject);
        }

        private IEnumerator ActivateOnEndOfFrame()
        {
            yield return new WaitForEndOfFrame();

            foreach (var i in _fGameObjects)
            {
                var gameObject = _fGameObjects.Get1(i);

                var entity = _fGameObjects.GetEntity(i);

                if (!ActivateObjects.Contains(gameObject.instance))
                {
                    if (entity.Has<SpawnEvent>()) entity.Del<SpawnEvent>();
                }
                else
                {
                    entity.Replace(new SpawnEvent());
                    entity.Replace(new ActiveObject());

                    gameObject.instance.transform.SetParent(_worldObject);

                    ActivateObjects.Remove(gameObject.instance);
                }
            }
        }

        private IEnumerator DeactivateOnEndOfFrame()
        {
            yield return new WaitForEndOfFrame();

            foreach (var i in _fInactiveObject)
            {
                var gameObject = _fInactiveObject.Get1(i);

                _fInactiveObject.GetEntity(i).Del<InactiveObject>();
                _generator.PoolManager.GetPoolContainer(gameObject.instance.name, true).Release(gameObject.instance);
            }
        }

        private void Deactivate(GameObject poolObject)
        {
            poolObject.transform.SetParent(_generator.PoolManager.GetPoolContainer(poolObject.name, true).Container);
        }
    }
}
