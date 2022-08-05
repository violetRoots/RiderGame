using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
using LeoEcsPhysics;
using DG.Tweening;
using RiderGame.World;
using RiderGame.RuntimeData;

namespace RiderGame.Gameplay
{
    public class EnemyCollisionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly SessionRuntimeData _sessionData;

        private readonly EcsFilter<EcsGameObject, Player> _fPlayer;
        private readonly EcsFilter<EcsGameObject, Enemy, ActiveObject> _fEnemy;
        private readonly EcsFilter<OnCollisionEnter2DEvent> _fOnCollisionEnter;

        private readonly List<GameObject> _enemies = new List<GameObject>();
        private readonly Dictionary<GameObject, OnCollisionEnter2DEvent> _collisionEnemies = new Dictionary<GameObject, OnCollisionEnter2DEvent>();
        private GameObject _playerObject;

        public void Init()
        {
            _playerObject = _fPlayer.Get1(0).instance;
        }

        public void Run()
        {
            _collisionEnemies.Clear();
            foreach(var i in _fOnCollisionEnter)
            {
                ref var eventData = ref _fOnCollisionEnter.Get1(i);

                var senderObject = eventData.senderGameObject;
                var collisionObject = eventData.collider2D.gameObject;

                if (!_enemies.Contains(senderObject)) continue;

                if (_collisionEnemies.ContainsKey(senderObject)) continue;

                if (collisionObject == _playerObject)
                {
                    _sessionData.Status.Value = SessionStatus.Ended;
                }
                else
                {
                    _collisionEnemies.Add(senderObject, eventData);
                }
            }

            _enemies.Clear();
            foreach (var i in _fEnemy)
            {
                ref var gameObject = ref _fEnemy.Get1(i);
                ref var enemy = ref _fEnemy.Get2(i);

                _enemies.Add(gameObject.instance);

                if (!_collisionEnemies.ContainsKey(gameObject.instance)) continue;

                var eventData = _collisionEnemies.GetValueOrDefault(gameObject.instance);
                ChangeDirection(ref enemy, eventData);
                PushEnemy(ref enemy, gameObject.instance, eventData);
            }
        }

        private void ChangeDirection(ref Enemy enemy, OnCollisionEnter2DEvent eventData)
        {
            var direction = Quaternion.Euler(0, 0, enemy.MovementDirectionAngle) * Vector2.down;
            direction = Vector2.Reflect(direction, eventData.firstContactPoint2D.normal);

            var resAngle = Vector2.SignedAngle(Vector2.down, direction);
            enemy.MovementDirectionAngle = resAngle;
        }

        private void PushEnemy(ref Enemy enemy, GameObject enemyGameObject, OnCollisionEnter2DEvent eventData)
        {
            Vector3 offset = eventData.firstContactPoint2D.normal * enemy.enemyConfiguration.PushForce;
            enemyGameObject.transform.DOMove(enemyGameObject.transform.position + offset, enemy.enemyConfiguration.PushTime).SetEase(Ease.Linear);
        }
    }
}
