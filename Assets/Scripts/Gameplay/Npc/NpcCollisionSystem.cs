using System;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.Ecs;
using LeoEcsPhysics;
using DG.Tweening;
using RiderGame.World;
using RiderGame.RuntimeData;

namespace RiderGame.Gameplay
{
    public class NpcCollisionSystem : IEcsRunSystem
    {
        private readonly GameplayRuntimeData _gameplayRuntimeData;
        private readonly SessionRuntimeData _sessionRuntimeData;

        private readonly EcsFilter<EcsGameObject, Npc, PlayerMovement, ActiveObject> _fEnemy;
        private readonly EcsFilter<OnCollisionEnter2DEvent> _fOnCollisionEnter;

        public void Run()
        {
            foreach (var i in _fOnCollisionEnter)
            {
                ref var eventData = ref _fOnCollisionEnter.Get1(i);

                var senderObject = eventData.senderGameObject;
                var collisionObject = eventData.collider2D.gameObject;

                if (!senderObject.FindActiveEntityWithComponent(out Npc npc)) continue;

                if (collisionObject.FindActiveEntityWithComponent<Player>(out EcsEntity playerEntity) && !playerEntity.Has<Invulnerability>())
                {
                    npc.StateController.TrySetActiveStateAs<StunnedState>();

                    EndSession(ref playerEntity);
                }
                else if (npc.StateController.TryGetActiveStateAs(out WalkState walkState))
                {
                    ChangeDirection(walkState, eventData);
                    PushEnemy(ref npc, senderObject, eventData);
                }
                else if (npc.StateController.TryGetActiveStateAs(out AggressionState aggressionState))
                {
                    npc.StateController.TrySetActiveStateAs<StunnedState>();
                }
            }
        }

        private void EndSession(ref EcsEntity playerEntity)
        {
            _gameplayRuntimeData.SetWorldIsMovingValue(false);

            Action endSessionCallback = () => _sessionRuntimeData.Status.Value = SessionStatus.Ended;

            ref var player = ref playerEntity.Get<Player>();
            var enemyCollisionAnimation = player.character.EnemyCollisionAnimationConfigs.GetAnimationByAngle(_gameplayRuntimeData.MovementDirection);
            BaseAnimatorControllerSystem.AddAnimation(playerEntity, enemyCollisionAnimation.animation, CharacterAnimationPriority.EnemyCollision, onEndPlay: endSessionCallback);
        }

        private void ChangeDirection(MovableState movableState, OnCollisionEnter2DEvent eventData)
        {
            var direction = Quaternion.Euler(0, 0, movableState.DirectionAngle) * Vector2.down;
            direction = Vector2.Reflect(direction, eventData.firstContactPoint2D.normal);

            var resAngle = Vector2.SignedAngle(Vector2.down, direction);
            movableState.DirectionAngle = resAngle;
        }

        private void PushEnemy(ref Npc npc, GameObject enemyGameObject, OnCollisionEnter2DEvent eventData)
        {
            Vector3 offset = eventData.firstContactPoint2D.normal * npc.npcConfiguration.PushForce;
            enemyGameObject.transform.DOMove(enemyGameObject.transform.position + offset, npc.npcConfiguration.PushTime).SetEase(Ease.Linear);
        }
    }
}
