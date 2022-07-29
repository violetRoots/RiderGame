using Leopotam.Ecs;
using RiderGame.World;
using UnityEngine;

namespace RiderGame.Gameplay
{
    public class EnemyMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EcsGameObject, Enemy, SpawnEvent> _fSpawnEnemy;
        private readonly EcsFilter<EcsGameObject, Enemy, ActiveObject> _fActiveEnemy;

        public void Run()
        {
            foreach(var i in _fSpawnEnemy)
            {
                ref var enemy = ref _fSpawnEnemy.Get2(i);

                var clampDirection = enemy.enemyConfiguration.ClampAngle;
                enemy.MovementDirectionAngle = Random.Range(-clampDirection, clampDirection);
            }

            foreach(var i in _fActiveEnemy)
            {
                ref var gameObject = ref _fActiveEnemy.Get1(i);
                ref var enemy = ref _fActiveEnemy.Get2(i);

                var transform = gameObject.instance.transform;
                var translation = Quaternion.Euler(0, 0, enemy.MovementDirectionAngle) * Vector2.down * enemy.enemyConfiguration.MovementSpeed;
                transform.Translate(translation * Time.deltaTime);
            }
        }
    }
}
