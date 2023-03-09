using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Leopotam.Ecs;
using LeoEcsPhysics;
using RiderGame.World;
using RiderGame.SO;
using RiderGame.RuntimeData;

namespace RiderGame.Gameplay
{
    public class ObstacleCollisionSystem : IEcsInitSystem, IEcsRunSystem
    {
        private const float DropPosYOffset = 2.0f;
        private const int MaxSpawnAttempts = 100;

        private readonly SessionRuntimeData _sessionRuntimeData;
        private readonly GameplayRuntimeData _gameplayRuntimeData;
        private readonly GameConfiguration _gameConfigs;

        private readonly EcsFilter<OnCollisionEnter2DEvent> _fCollisionEnter;
        private readonly EcsFilter<EcsGameObject, Obstacle> _fObstacle;

        private GeneralCharacterConfiguration _generalCharacterConfigs;
        private GameObject _worldObject;

        public void Init()
        {
            _generalCharacterConfigs = _gameConfigs.GeneralCharacterConfiguration;
        }

        public void Run()
        {
            foreach (var i in _fCollisionEnter)
            {
                ref var eventData = ref _fCollisionEnter.Get1(i);

                if (!eventData.senderGameObject.FindActiveEntityWithComponent<Player>(out EcsEntity playerEntity)) continue;

                var collisionObject = eventData.collider2D.gameObject;
                ref var player = ref playerEntity.Get<Player>();

                if (collisionObject.FindActiveEntityWithComponent<Obstacle>() && !playerEntity.Has<Invulnerability>())
                {
                    PushPlayer(ref eventData);
                    DropCoins(ref eventData);

                    InvulnerabilityEffect.AddToEntity(playerEntity, _generalCharacterConfigs.InvunerabilityDuration);
                    BlinkingEffect.AddToEntity(playerEntity, _generalCharacterConfigs.InvunerabilityDuration, _generalCharacterConfigs.InvunerabilityBlinkInterval, player.renderer);

                    var collisionAnimation = player.character.ObstacleCollisionAnimationConfigs.GetAnimationByAngle(_gameplayRuntimeData.MovementDirection);
                    BaseAnimatorControllerSystem.AddAnimation(playerEntity, collisionAnimation.animation, PlayerAnimationPriority.ObstacleCollision);
                }
            }
        }

        private void PushPlayer(ref OnCollisionEnter2DEvent eventData)
        {
            Vector3 offset = -eventData.firstContactPoint2D.normal * _generalCharacterConfigs.PushForce;
            MoveWorldObjectSystem.MoveWorldObjectByOffset(offset, _generalCharacterConfigs.PushTime, Ease.OutExpo);
        }

        private void DropCoins(ref OnCollisionEnter2DEvent eventData)
        {
            int droppedCoinsCount = Random.Range(_generalCharacterConfigs.CoinsDropCount.x, _generalCharacterConfigs.CoinsDropCount.y);

            droppedCoinsCount = Mathf.Clamp(droppedCoinsCount, 0, _sessionRuntimeData.CoinsCount.Value);
            _sessionRuntimeData.CoinsCount.Value = droppedCoinsCount;

            var coinColliderRadius = _generalCharacterConfigs.CoinPrefab.Value.collider.radius;
            var dropCoinsSpawnPos = eventData.firstContactPoint2D.point;
            dropCoinsSpawnPos.y = DropPosYOffset;

            for (var i = 0; i < droppedCoinsCount; i++)
            {
                for(var j = 0; j <= MaxSpawnAttempts; j++)
                {
                    var randCoinPos = dropCoinsSpawnPos + Random.insideUnitCircle * _generalCharacterConfigs.CoinsDropRadius;

                    if (Physics2D.OverlapCircle(randCoinPos, coinColliderRadius) == null)
                    {
                        var coin = ObjectActivationSystem.Instantiate(_generalCharacterConfigs.CoinPrefab.gameObject, dropCoinsSpawnPos);
                        coin.transform.DOMove(randCoinPos, _generalCharacterConfigs.CoinsDropTime);
                        break;
                    }

                    if (j == MaxSpawnAttempts)
                        Debug.Log("not enough space to spawn coin");
                }
            }
        }
    }
}
