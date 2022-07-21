using System.Linq;
using UnityEngine;
using Leopotam.Ecs;
using RiderGame.RuntimeData;
using SkyCrush.Utility;
using AnimationInfo = RiderGame.SO.AnimationInfo;

namespace RiderGame.Gameplay
{
    public class CharacterAnimationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly GameplayRuntimeData _levelData;

        private readonly EcsFilter<Player, CharacterAnimation> _animationFilter;

        private AnimationInfo _currentAnimationInfo;

        public void Init()
        {
            foreach (var i in _animationFilter)
            {
                ref var player = ref _animationFilter.Get1(i);
                ref var animationComponent = ref _animationFilter.Get2(i);

                var animationsInfo = player.character.AnimationsInfo;
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
                ref var player = ref _animationFilter.Get1(i);
                ref var animationComponent = ref _animationFilter.Get2(i);

                var minDifference = float.MaxValue;
                foreach(var animationInfo in player.character.AnimationsInfo)
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
                animator.Play(_currentAnimationInfo.animation, continueFrame: true);
            }
        }
    }
}
