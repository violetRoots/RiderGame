using Leopotam.Ecs;
using RiderGame.World;
using UnityEngine;

namespace RiderGame.Gameplay
{
    public class EnemyMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EcsGameObject, Enemy, ActivationEvent> _fEnemyActivationEvent;
        private readonly EcsFilter<EcsGameObject, Enemy, ActiveObject> _fActiveEnemy;

        public void Run()
        {
            foreach(var i in _fEnemyActivationEvent)
            {
                ref var enemy = ref _fEnemyActivationEvent.Get2(i);

                var clampDirection = enemy.enemyConfiguration.ClampAngle;
                enemy.MovementDirectionAngle = Random.Range(-clampDirection, clampDirection);
            }

            foreach(var i in _fActiveEnemy)
            {
                ref var gameObject = ref _fActiveEnemy.Get1(i);
                ref var enemy = ref _fActiveEnemy.Get2(i);

                if (enemy.state == EnemyState.Stunned) continue;

                var transform = gameObject.instance.transform;
                var translation = Quaternion.Euler(0, 0, enemy.MovementDirectionAngle) * Vector2.down;

                if (enemy.state == EnemyState.Normal)
                {
                    translation *= enemy.enemyConfiguration.MovementSpeed;
                }
                else if (enemy.state == EnemyState.Agressive)
                {
                    translation *= enemy.enemyConfiguration.AgressionMovementSpeed;
                }

                transform.Translate(translation * Time.deltaTime);
            }
        }
    }
}
