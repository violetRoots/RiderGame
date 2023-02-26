using UnityEngine;
using Leopotam.Ecs;
using RiderGame.World;

namespace RiderGame.Gameplay
{
    public class BaseNpcStateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EcsGameObject, Npc, ActivationEvent> _fNpcActivationEvent;

        public void Run()
        {
            foreach(var i in _fNpcActivationEvent)
            {
                var npc = _fNpcActivationEvent.Get2(i);

                npc.StateController.Init();
            }
        }
    }
}
