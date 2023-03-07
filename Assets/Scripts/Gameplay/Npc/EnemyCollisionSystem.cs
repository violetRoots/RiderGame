using System;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
using LeoEcsPhysics;
using DG.Tweening;
using RiderGame.World;
using RiderGame.SO;
using RiderGame.RuntimeData;

namespace RiderGame.Gameplay
{
    public class EnemyCollisionSystem : IEcsRunSystem
    {
        private readonly GameplayRuntimeData _gameplayRuntimeData;
        private readonly SessionRuntimeData _sessionRuntimeData;

        private readonly EcsFilter<EcsGameObject, Npc, PlayerMovement, ActiveObject> _fEnemy;
        private readonly EcsFilter<OnCollisionEnter2DEvent> _fOnCollisionEnter;

        private readonly Dictionary<GameObject, OnCollisionEnter2DEvent> _collisionEnemies = new Dictionary<GameObject, OnCollisionEnter2DEvent>();

        public void Run()
        {
            //_collisionEnemies.Clear();
            //foreach(var i in _fOnCollisionEnter)
            //{
            //    ref var eventData = ref _fOnCollisionEnter.Get1(i);

            //    var senderObject = eventData.senderGameObject;
            //    var collisionObject = eventData.collider2D.gameObject;

            //    if (!senderObject.FindActiveEntityWithComponent<Npc>()) continue;

            //    if (_collisionEnemies.ContainsKey(senderObject)) continue;

            //    if (collisionObject.FindActiveEntityWithComponent<Player>(out EcsEntity playerEntity) && !playerEntity.Has<Invulnerability>())
            //    {
            //        _gameplayRuntimeData.SetWorldIsMovingValue(false);

            //        Action endSessionCallback = () => _sessionRuntimeData.Status.Value = SessionStatus.Ended;

            //        ref var player = ref playerEntity.Get<Player>();
            //        var enemyCollisionAnimation = player.character.EnemyCollisionAnimationConfigs.GetAnimationByAngle(_gameplayRuntimeData.MovementDirection);
            //        BaseAnimatorControllerSystem.AddAnimation(playerEntity, enemyCollisionAnimation.animation, CharacterAnimationPriority.EnemyCollision, onEndPlay: endSessionCallback);
            //    }
            //    else
            //    {
            //        _collisionEnemies.Add(senderObject, eventData);
            //    }
            //}

            //foreach (var i in _fEnemy)
            //{
            //    ref var gameObject = ref _fEnemy.Get1(i);
            //    ref var enemy = ref _fEnemy.Get2(i);
            //    ref var movement = ref _fEnemy.Get3(i);

            //    if (!_collisionEnemies.ContainsKey(gameObject.instance)) continue;

            //    var eventData = _collisionEnemies.GetValueOrDefault(gameObject.instance);

            //    if (enemy.state == EnemyState.Normal)
            //    {
            //        ChangeDirection(ref movement, eventData);
            //        PushEnemy(ref enemy, gameObject.instance, eventData);
            //    }
            //    else if(enemy.state == EnemyState.Agressive)
            //    {
            //        stateController.TrySetActiveStateAs<StunnedState>();
            //    }
            //}
        }

        private void ChangeDirection(ref PlayerMovement movement, OnCollisionEnter2DEvent eventData)
        {
            var direction = Quaternion.Euler(0, 0, movement.DirectionAngle) * Vector2.down;
            direction = Vector2.Reflect(direction, eventData.firstContactPoint2D.normal);

            var resAngle = Vector2.SignedAngle(Vector2.down, direction);
            movement.DirectionAngle = resAngle;
        }

        private void PushEnemy(ref Npc enemy, GameObject enemyGameObject, OnCollisionEnter2DEvent eventData)
        {
            Vector3 offset = eventData.firstContactPoint2D.normal * enemy.npcConfiguration.PushForce;
            enemyGameObject.transform.DOMove(enemyGameObject.transform.position + offset, enemy.npcConfiguration.PushTime).SetEase(Ease.Linear);
        }
    }
}
