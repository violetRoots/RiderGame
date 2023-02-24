using Leopotam.Ecs;
using RiderGame.SO;
using RiderGame.World;
using UnityEngine;

namespace RiderGame.Gameplay
{
    public class NpcWalkStateSystem : IEcsRunSystem
    {
        private readonly GameConfiguration _gameConfigs;

        private readonly EcsFilter<EcsGameObject, Npc, ActiveObject> _fNpc;
        private readonly EcsFilter<EcsGameObject, Npc, ActivationEvent> _fNpcActivationEvent;

        public void Run()
        {
            foreach(var i in _fNpcActivationEvent)
            {
                ref var npc = ref _fNpcActivationEvent.Get2(i);

                if (!npc.StateController.TryGet(out WalkState walkState)) continue;

                walkState.SetClampDirectionAngle(_gameConfigs.ClampDirectionAngle);
            }

            foreach(var i in _fNpc)
            {
                ref var gameObject = ref _fNpc.Get1(i);
                ref var npc = ref _fNpc.Get2(i);

                var stateController = npc.StateController;

                if (!stateController.TryGetActiveStateAs(out WalkState walkState)) continue;

                var transform = gameObject.instance.transform;
                var translation = Quaternion.Euler(0, 0, walkState.DirectionAngle) * Vector2.down * walkState.MovementSpeed;

                transform.Translate(translation * Time.deltaTime);
            }
        }
    }
}
