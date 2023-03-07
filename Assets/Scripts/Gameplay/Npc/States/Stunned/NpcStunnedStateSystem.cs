using Leopotam.Ecs;
using UniRx;
using UnityEngine;
using RiderGame.World;

namespace RiderGame.Gameplay
{
    public class NpcStunnedStateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EcsGameObject, Npc, ActivationEvent> _fNpcActivationEvent;

        public void Run()
        {
            foreach(var i in _fNpcActivationEvent)
            {
                var entity = _fNpcActivationEvent.GetEntity(i);

                var npc = _fNpcActivationEvent.Get2(i);

                if (!npc.StateController.TryGet(out StunnedState stunnedState)) continue;

                stunnedState.changeStateSubscription = npc.StateController.ActiveState.Subscribe((state) => OnStateChanged(entity, npc, state));
            }
        }

        private void OnStateChanged(EcsEntity entity, Npc npc, State newState)
        {
            var stunnedState = newState as StunnedState;
            npc.stunnedStateRefs.icon.gameObject.SetActive(stunnedState != null);

            if (stunnedState == null) return;

            var currentAnimationInfo = stunnedState.stunnedAnimation.GetAnimationByAngle(stunnedState.DirectionAngle);
            BaseAnimatorControllerSystem.AddAnimation(entity, currentAnimationInfo.animation, NpcAnimationPriority.Stunned, true, flipDir: currentAnimationInfo.FlipValue);
        }
    }
}
