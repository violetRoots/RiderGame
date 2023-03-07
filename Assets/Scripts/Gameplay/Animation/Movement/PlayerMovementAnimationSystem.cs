using Leopotam.Ecs;
using RiderGame.World;

namespace RiderGame.Gameplay
{
    public class PlayerMovementAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BaseAnimatorController, PlayerMovement, MovementAnimation, ActiveObject>.Exclude<IgnoreMovementAnimation> _animationFilter;

        public void Run()
        {
            foreach (var i in _animationFilter)
            {
                ref var entity = ref _animationFilter.GetEntity(i);
                ref var animatorController = ref _animationFilter.Get1(i);
                ref var movement = ref _animationFilter.Get2(i);
                ref var movementAnimation = ref _animationFilter.Get3(i);

                var currentAnimationInfo = movementAnimation.animationConfiguration.GetAnimationByAngle(movement.DirectionAngle);

                var flipValue = currentAnimationInfo.isFlip ? -1 : 1;
                BaseAnimatorControllerSystem.AddAnimation(entity, currentAnimationInfo.animation, PlayerAnimationPriority.Walk, loop: true, continueFrame: true, flipDir: flipValue);
            }
        }
    }
}
