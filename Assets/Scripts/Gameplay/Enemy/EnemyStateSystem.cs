using UnityEngine;
using Leopotam.Ecs;
using RiderGame.World;
using System.Linq;

namespace RiderGame.Gameplay
{
    public class EnemyStateSystem : IEcsInitSystem, IEcsRunSystem
    {
        private const int RaysCount = 9;
        private const float MaxAgressionRadius = 15.0f;

        private readonly EcsFilter<EcsGameObject, Player> _fPlayer;
        private readonly EcsFilter<EcsGameObject, Enemy, Movement, ActiveObject> _fEnemy;
        private readonly EcsFilter<EcsGameObject, Enemy, DeactivationEvent> _fEnemyDeactivationEvent;

        private GameObject _playerObject;

        public void Init()
        {
            _playerObject = _fPlayer.Get1(0).instance;
        }

        public void Run()
        {
            foreach (var i in _fEnemy)
            {
                ref var gameObject = ref _fEnemy.Get1(i);
                ref var enemy = ref _fEnemy.Get2(i);
                ref var movement = ref _fEnemy.Get3(i);

                if (enemy.state == EnemyState.Normal)
                {
                    if (enemy.enemyConfiguration.AgressionRadius == 0.0f) continue;

                    var raycastOrigin = gameObject.instance.transform.position;
                    var angleStep = ((float)(enemy.enemyConfiguration.ClampAngle * 2) / (RaysCount - 1));

                    for (var rayIndex = 0; rayIndex < RaysCount; rayIndex++)
                    {
                        var angle = enemy.enemyConfiguration.ClampAngle - rayIndex * angleStep;
                        var raycastDirection = Quaternion.Euler(0, 0, angle) * Vector2.down;
                        var hits = Physics2D.RaycastAll(raycastOrigin, raycastDirection, enemy.enemyConfiguration.AgressionRadius);

                        if (hits.Count((hit) => hit.collider.gameObject == _playerObject) == 0) continue;

                        SetAgressiveState(ref enemy);
                        break;
                    }
                }
                else if(enemy.state == EnemyState.Agressive)
                {
                    var toPlayer = _playerObject.transform.position - gameObject.instance.transform.position;
                    var movementAngle = Vector2.SignedAngle(Vector2.down, toPlayer);
                    movement.DirectionAngle = movementAngle;

                    if (toPlayer.magnitude > MaxAgressionRadius)
                    {
                        SetNormalState(ref enemy);
                    }
                }
            }

            foreach (var i in _fEnemyDeactivationEvent)
            {
                ref var enemy = ref _fEnemyDeactivationEvent.Get2(i);

                SetNormalState(ref enemy);
            }
        }

        public static void SetNormalState(ref Enemy enemy)
        {
            enemy.state = EnemyState.Normal;

            enemy.agressionIcon.gameObject.SetActive(false);
            enemy.stunnedIcon.gameObject.SetActive(false);
        }

        public static void SetAgressiveState(ref Enemy enemy)
        {
            enemy.state = EnemyState.Agressive;

            enemy.agressionIcon.gameObject.SetActive(true);
            enemy.stunnedIcon.gameObject.SetActive(false);
        }

        public static void SetStunnedState(ref Enemy enemy)
        {
            enemy.state = EnemyState.Stunned;

            enemy.agressionIcon.gameObject.SetActive(false);
            enemy.stunnedIcon.gameObject.SetActive(true);
        }
    }
}
