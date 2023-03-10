using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Leopotam.Ecs;
using SkyCrush.Utility;
using RiderGame.World;

namespace RiderGame.Gameplay
{
    public class BaseAnimatorControllerSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BaseAnimatorController, ActiveObject> _fAnimator;
        private readonly EcsFilter<BaseAnimatorController, DeactivationEvent> _fAnimtorDeactivationEvent;

        public static void AddAnimation(EcsEntity entity,
                                        SpriteAnimation animation,
                                        PlayerAnimationPriority priority,
                                        bool loop = false,
                                        bool continueFrame = false,
                                        int flipDir = 0,
                                        Action onEndPlay = null)
        {
            AddAnimation(entity, animation, (int)priority, loop, continueFrame, flipDir, onEndPlay);
        }

        public static void AddAnimation(EcsEntity entity,
                                SpriteAnimation animation,
                                NpcAnimationPriority priority,
                                bool loop = false,
                                bool continueFrame = false,
                                int flipDir = 0,
                                Action onEndPlay = null)
        {
            AddAnimation(entity, animation, (int)priority, loop, continueFrame, flipDir, onEndPlay);
        }

        private static void AddAnimation(EcsEntity entity, 
                                        SpriteAnimation animation,  
                                        int priority, 
                                        bool loop = false, 
                                        bool continueFrame = false, 
                                        int flipDir = 0, 
                                        Action onEndPlay = null)
        {
            if (!TryGetBaseAnimatorController(entity, out BaseAnimatorController animatorController))
            {
                Debug.LogError($"You should add {typeof(BaseAnimatorController)} to entity before play animation");
                return;
            }

            BaseAnimatorController.AnimationInfo animationInfo = null;
            animationInfo = new BaseAnimatorController.AnimationInfo()
            {
                priority = priority,
                animation = animation,
                loop = loop,
                continueFrame = continueFrame,
                flipDir = flipDir,
                onEndPlay = () =>
                {
                    if (!loop && animatorController.animationsInfo.Contains(animationInfo))
                        animatorController.animationsInfo.Remove(animationInfo);

                    onEndPlay?.Invoke();
                }
            };

            var samePriorityAnimationInfo = animatorController.animationsInfo.Find(info => info.priority == animationInfo.priority);
            if(samePriorityAnimationInfo != null)
                animatorController.animationsInfo.Remove(samePriorityAnimationInfo);
            animatorController.animationsInfo.Add(animationInfo);

            entity.Replace(animatorController);
        }

        public static bool TryGetBaseAnimatorController(EcsEntity entity, out BaseAnimatorController animatorController)
        {
            if (!entity.Has<BaseAnimatorController>())
            {
                animatorController = default;
                return false;
            }

            animatorController = entity.Get<BaseAnimatorController>();
            if (animatorController.animationsInfo == null)
                animatorController.animationsInfo = new List<BaseAnimatorController.AnimationInfo>();

            return true;
        }

        public void Run()
        {
            foreach(var i in _fAnimator)
            {
                ref var animatorController = ref _fAnimator.Get1(i);

                var animsInfo = animatorController.animationsInfo;

                if (animsInfo == null) continue;

                if (animsInfo.Count > 1)
                    animsInfo = animsInfo.OrderByDescending(info => (int) info.priority).ToList();

                if (animsInfo[0] != null)
                    animatorController.currentAnimationInfo = animsInfo[0];

                if (animatorController.spriteAnimator.CurrentAnimation == animatorController.currentAnimationInfo.animation 
                    && (animatorController.spriteAnimator.Flipped !^ animatorController.currentAnimationInfo.flipDir == -1)) continue;

                PlayAnimation(animatorController.spriteAnimator, animatorController.currentAnimationInfo);
            }

            foreach(var i in _fAnimtorDeactivationEvent)
            {
                ref var animatorController = ref _fAnimtorDeactivationEvent.Get1(i);

                animatorController.animationsInfo.Clear();

            }
        }

        private static void PlayAnimation(SpriteAnimator animator, BaseAnimatorController.AnimationInfo animationInfo)
        {
            if (animationInfo.flipDir != 0)
                animator.FlipTo(animationInfo.flipDir);

            animator.Play(animationInfo.animation, animationInfo.loop, animationInfo.continueFrame, onEndAnimationAction: animationInfo.onEndPlay);
        }
    }
}
