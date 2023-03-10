using UnityEngine;
using UniRx;
using Leopotam.Ecs;
using RiderGame.World;

namespace RiderGame.Gameplay
{
    public class NpcIdleStateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EcsGameObject, Npc, ActivationEvent> _fNpcActivationEvent;

        public void Run()
        {
            foreach(var i in _fNpcActivationEvent)
            {
                var entity = _fNpcActivationEvent.GetEntity(i);
                var npc = _fNpcActivationEvent.Get2(i);

                npc.StateController.ActiveState.Subscribe((state) => OnActiveStateValueChanged(entity, state));
            }
        }

        private void OnActiveStateValueChanged(EcsEntity entity, State newValue)
        {
            if(newValue is IdleState idleState)
            {
                var currentAnimationConfig = idleState.idleAnimation.GetAnimationByAngle(idleState.DirectionAngle);
                BaseAnimatorControllerSystem.AddAnimation(entity, currentAnimationConfig.animation, NpcAnimationPriority.Idle, true, flipDir: currentAnimationConfig.FlipValue);
            }
            else
            {
                BaseAnimatorControllerSystem.ClearAnimation(entity, NpcAnimationPriority.Idle);
            }
        }
    }
}
