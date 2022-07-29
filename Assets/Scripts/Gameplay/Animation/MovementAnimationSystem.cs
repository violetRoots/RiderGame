using UnityEngine;
using Leopotam.Ecs;
using SkyCrush.Utility;
using AnimationInfo = RiderGame.SO.AnimationInfo;
using RiderGame.World;

namespace RiderGame.Gameplay
{
    public class MovementAnimationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<MovementAnimation, ActiveObject> _animationFilter;

        private AnimationInfo _currentAnimationInfo;

        public void Init()
        {
            foreach (var i in _animationFilter)
            {
                ref var animationComponent = ref _animationFilter.Get1(i);

                var animationsInfo = animationComponent.animationConfiguration.AnimationsInfo;
                SpriteAnimation[] animations = new SpriteAnimation[animationsInfo.Length];

                for (var j = 0; j < animationsInfo.Length; j++)
                {
                    animations[j] = animationsInfo[j].animation;
                }

                animationComponent.spriteAnimator.SetAnimations(animations);
            }
        }

        public void Run()
        {
            foreach (var i in _animationFilter)
            {
                ref var animationComponent = ref _animationFilter.Get1(i);

                var minDifference = float.MaxValue;
                foreach(var animationInfo in animationComponent.animationConfiguration.AnimationsInfo)
                {
                    var difference = Mathf.Abs(animationComponent.directionAngle - animationInfo.angle);

                    if(difference < minDifference)
                    {
                        minDifference = difference;
                        _currentAnimationInfo = animationInfo;
                    }
                }

                var animator = animationComponent.spriteAnimator;

                if (animator.CurrentAnimation == _currentAnimationInfo.animation && animator.Flipped !^ _currentAnimationInfo.isFlip) continue;

                var flipValue = _currentAnimationInfo.isFlip ? -1 : 1;
                animator.FlipTo(flipValue);
                animator.Play(_currentAnimationInfo.animation, continueFrame: true);
            }
        }
    }
}
