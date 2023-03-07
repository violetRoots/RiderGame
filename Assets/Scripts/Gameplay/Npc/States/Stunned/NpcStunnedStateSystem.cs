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
                var npc = _fNpcActivationEvent.Get2(i);

                if (!npc.StateController.TryGet(out StunnedState stunnedState)) continue;

                stunnedState.changeStateSubscription = npc.StateController.ActiveState.Subscribe((state) => OnStateChanged(npc, state));
            }
        }

        private void OnStateChanged(Npc npc, State newState)
        {
            var stunnedIconActive = newState is StunnedState;
            npc.stunnedStateRefs.icon.gameObject.SetActive(stunnedIconActive);
        }
    }
}
