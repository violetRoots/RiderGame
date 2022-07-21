using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Leopotam.Ecs;
using LeoEcsPhysics;
using RiderGame.World;
using RiderGame.SO;

namespace RiderGame.Gameplay
{
    public class ObstacleCollisionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly GameConfiguration _gameConfigs;

        private readonly EcsFilter<EcsGameObject, Player> _fPlayer;
        private readonly EcsFilter<EcsGameObject, MoveWorldObject> _fWorldObject;
        private readonly EcsFilter<EcsGameObject, ActiveObject, Obstacle> _fObstacle;
        private readonly EcsFilter<OnCollisionEnter2DEvent> _fCollisionEnter;

        private PlayerConfiguration _playerConfigs;
        private EcsEntity _playerEntity;
        private Player _player;
        private GameObject _playerObject;
        private GameObject _worldObject;
        private readonly List<GameObject> _obstacles = new List<GameObject>();

        public void Init()
        {
            _playerConfigs = _gameConfigs.PlayerConfiguration;
            _playerEntity = _fPlayer.GetEntity(0);
            _playerObject = _fPlayer.Get1(0).instance;
            _player = _fPlayer.Get2(0);
            _worldObject = _fWorldObject.Get1(0).instance;
        }

        public void Run()
        {
            _obstacles.Clear();
            foreach (var i in _fObstacle)
            {
                ref var gameObject = ref _fObstacle.Get1(i);

                _obstacles.Add(gameObject.instance);
            }

            foreach (var i in _fCollisionEnter)
            {
                ref var eventData = ref _fCollisionEnter.Get1(i);

                if (eventData.senderGameObject != _playerObject) return;

                var collisionObject = eventData.collider2D.gameObject;

                if (_obstacles.Contains(collisionObject) && !_playerEntity.Has<Invulnerability>())
                {
                     Vector3 offset = eventData.firstContactPoint2D.normal * _playerConfigs.PushForce;
                    _worldObject.transform.DOMove(_worldObject.transform.position - offset, _playerConfigs.PushTime);

                    InvulnerabilitySystem.AddInvulnerablility(ref _playerEntity, _playerConfigs, _player.renderer);
                }
            }
        }
    }
}
