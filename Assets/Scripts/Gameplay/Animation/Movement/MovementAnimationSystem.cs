using UnityEngine;
using Leopotam.Ecs;
using SkyCrush.Utility;
using AnimationInfo = RiderGame.SO.SpriteAnimationConfiguration.AnimationConfigurationInfo;
using RiderGame.World;

namespace RiderGame.Gameplay
{
    public class MovementAnimationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BaseAnimatorController, Movement, MovementAnimation, ActiveObject>.Exclude<IgnoreMovementAnimation> _animationFilter;

        private AnimationInfo _currentAnimationInfo;

        public void Run()
        {
            foreach (var i in _animationFilter)
            {
                ref var entity = ref _animationFilter.GetEntity(i);
                ref var animatorController = ref _animationFilter.Get1(i);
                ref var movement = ref _animationFilter.Get2(i);
                ref var movementAnimation = ref _animationFilter.Get3(i);

                _currentAnimationInfo = movementAnimation.animationConfiguration.GetAnimationByAngle(movement.DirectionAngle);

                short flipValue = (short) (_currentAnimationInfo.isFlip ? -1 : 1);
                BaseAnimatorControllerSystem.AddAnimation(entity, _currentAnimationInfo.animation, CharacterAnimationPriority.Movement, loop: true, continueFrame: true, flipDir: flipValue);
            }
        }
    }
}
