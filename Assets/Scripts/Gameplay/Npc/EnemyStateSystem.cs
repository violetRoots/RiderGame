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
        private readonly EcsFilter<EcsGameObject, Npc, Movement, ActiveObject> _fEnemy;
        private readonly EcsFilter<EcsGameObject, Npc, DeactivationEvent> _fEnemyDeactivationEvent;

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
                    if (enemy.npcConfiguration.AgressionRadius == 0.0f) continue;

                    var raycastOrigin = gameObject.instance.transform.position;
                    var angleStep = ((float)(enemy.npcConfiguration.ClampAngle * 2) / (RaysCount - 1));

                    for (var rayIndex = 0; rayIndex < RaysCount; rayIndex++)
                    {
                        var angle = enemy.npcConfiguration.ClampAngle - rayIndex * angleStep;
                        var raycastDirection = Quaternion.Euler(0, 0, angle) * Vector2.down;
                        var hits = Physics2D.RaycastAll(raycastOrigin, raycastDirection, enemy.npcConfiguration.AgressionRadius);

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

        public static void SetNormalState(ref Npc enemy)
        {
            enemy.state = EnemyState.Normal;

            //enemy.aggressionState.icon.gameObject.SetActive(false);
            //enemy.stunnedState.icon.gameObject.SetActive(false);
        }

        public static void SetAgressiveState(ref Npc enemy)
        {
            enemy.state = EnemyState.Agressive;

            //enemy.aggressionState.icon.gameObject.SetActive(true);
            //enemy.stunnedState.icon.gameObject.SetActive(false);
        }

        public static void SetStunnedState(ref Npc enemy)
        {
            enemy.state = EnemyState.Stunned;

            //enemy.aggressionState.icon.gameObject.SetActive(false);
            //enemy.stunnedState.icon.gameObject.SetActive(true);
        }
    }
}
