using UnityEngine;
using DG.Tweening;
using Leopotam.Ecs;
using LeoEcsPhysics;
using RiderGame.World;
using RiderGame.SO;
using RiderGame.RuntimeData;

namespace RiderGame.Gameplay
{
    public class ObstacleCollisionSystem
    {
        private const float DropPosYOffset = 2.0f;
        private const int MaxSpawnAttempts = 100;

        public static void PushPlayer(ref EcsEntity playerEntity,
                                      ref OnCollisionEnter2DEvent eventData, 
                                      SessionRuntimeData sessionRuntimeData, 
                                      GameplayRuntimeData gameplayRuntimeData, 
                                      GeneralCharacterConfiguration generalCharacterConfigs)
        {
            MoveWorldByNormal(ref eventData, generalCharacterConfigs);
            DropCoins(ref eventData, sessionRuntimeData, generalCharacterConfigs);

            var player = playerEntity.Get<Player>();

            InvulnerabilityEffect.AddToEntity(playerEntity, generalCharacterConfigs.InvunerabilityDuration);
            BlinkingEffect.AddToEntity(playerEntity, generalCharacterConfigs.InvunerabilityDuration, generalCharacterConfigs.InvunerabilityBlinkInterval, player.renderer);

            var collisionAnimation = player.character.ObstacleCollisionAnimationConfigs.GetAnimationByAngle(gameplayRuntimeData.MovementDirection);
            BaseAnimatorControllerSystem.AddAnimation(playerEntity, collisionAnimation.animation, PlayerAnimationPriority.ObstacleCollision);
        }

        private static void MoveWorldByNormal(ref OnCollisionEnter2DEvent eventData, GeneralCharacterConfiguration generalCharacterConfigs)
        {
            Vector3 offset = -eventData.firstContactPoint2D.normal * generalCharacterConfigs.PushForce;
            MoveWorldObjectSystem.MoveWorldObjectByOffset(offset, generalCharacterConfigs.PushTime, Ease.OutExpo);
        }

        private static void DropCoins(ref OnCollisionEnter2DEvent eventData, SessionRuntimeData sessionRuntimeData, GeneralCharacterConfiguration generalCharacterConfigs)
        {
            int droppedCoinsCount = Random.Range(generalCharacterConfigs.CoinsDropCount.x, generalCharacterConfigs.CoinsDropCount.y);

            droppedCoinsCount = Mathf.Clamp(droppedCoinsCount, 0, sessionRuntimeData.CoinsCount.Value);
            sessionRuntimeData.CoinsCount.Value = droppedCoinsCount;

            var coinColliderRadius = generalCharacterConfigs.CoinPrefab.Value.collider.radius;
            var dropCoinsSpawnPos = eventData.firstContactPoint2D.point;
            dropCoinsSpawnPos.y = DropPosYOffset;

            for (var i = 0; i < droppedCoinsCount; i++)
            {
                for(var j = 0; j <= MaxSpawnAttempts; j++)
                {
                    var randCoinPos = dropCoinsSpawnPos + Random.insideUnitCircle * generalCharacterConfigs.CoinsDropRadius;

                    if (Physics2D.OverlapCircle(randCoinPos, coinColliderRadius) == null)
                    {
                        var coin = ObjectActivationSystem.Instantiate(generalCharacterConfigs.CoinPrefab.gameObject, dropCoinsSpawnPos);
                        coin.transform.DOMove(randCoinPos, generalCharacterConfigs.CoinsDropTime);
                        break;
                    }

                    if (j == MaxSpawnAttempts)
                        Debug.Log("not enough space to spawn coin");
                }
            }
        }
    }
}
