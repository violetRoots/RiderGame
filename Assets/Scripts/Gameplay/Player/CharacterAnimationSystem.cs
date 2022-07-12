using System.Linq;
using UnityEngine;
using Leopotam.Ecs;
using RiderGame.Level;
using SkyCrush.Utility;
using AnimationInfo = RiderGame.SO.AnimationInfo;

namespace RiderGame.Gameplay
{
    public class CharacterAnimationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly RuntimeLevelData _levelData;

        private readonly EcsFilter<CharacterAnimation> _animationFilter;

        private AnimationInfo _currentAnimationInfo;

        public void Init()
        {
            foreach (var i in _animationFilter)
            {
                ref var animationComponent = ref _animationFilter.Get1(i);

                var animationsInfo = animationComponent.character.AnimationsInfo;
                SpriteAnimation[] animations = new SpriteAnimation[animationsInfo.Length]; ;

                for (var j = 0; j < animationsInfo.Length; j++)
                {
                    animations[j] = animationsInfo[j].animation;
                }

                animationComponent.spriteAnimator.SetAnimations(animations);
            }
        }

        public void Run()
        {
            foreach(var i in _animationFilter)
            {
                ref var animationComponent = ref _animationFilter.Get1(i);

                var minDifference = float.MaxValue;
                foreach(var animationInfo in animationComponent.character.AnimationsInfo)
                {
                    var difference = Mathf.Abs(_levelData.MovementDirection - animationInfo.angle);

                    if(difference < minDifference)
                    {
                        minDifference = difference;
                        _currentAnimationInfo = animationInfo;
                    }
                }

                var animator = animationComponent.spriteAnimator;

                if (animator.CurrentAnimation == _currentAnimationInfo.animation) return;

                var flipValue = _currentAnimationInfo.isFlip ? -1 : 1;
                animator.FlipTo(flipValue);
                animator.Play(_currentAnimationInfo.animation);
            }
        }
    }
}
