using Leopotam.Ecs;
using RiderGame.World;

namespace RiderGame.Gameplay
{
    public class EnemyAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Enemy, MovementAnimation, ActiveObject> _fEnemyAnimation;

        public void Run()
        {
            foreach(var i in _fEnemyAnimation)
            {
                ref var enemy = ref _fEnemyAnimation.Get1(i);
                ref var animationComponent = ref _fEnemyAnimation.Get2(i);

                animationComponent.directionAngle = enemy.MovementDirectionAngle;
            }
        }
    }
}
