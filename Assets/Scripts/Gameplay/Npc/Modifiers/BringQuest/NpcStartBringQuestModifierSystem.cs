using UnityEngine;
using Leopotam.Ecs;
using RiderGame.SO;
using RiderGame.World;

namespace RiderGame.Gameplay
{
    public class NpcStartBringQuestModifierSystem : IEcsRunSystem
    {
        private readonly GameConfiguration _gameConfigs;

        private readonly EcsFilter<EcsGameObject, Npc, ActivationEvent> _fNpcActivationEvent;

        public void Run()
        {
            foreach(var i in _fNpcActivationEvent)
            {
                ref var gameObject = ref _fNpcActivationEvent.Get1(i);
                ref var npc = ref _fNpcActivationEvent.Get2(i);

                if (!npc.ModifierController.TryGet(out StartBringQuestModifier startBringQuestModifier)) continue;

                var generalNpcConfigs = _gameConfigs.GeneralNpcConfiguration;

                var transform = gameObject.instance.transform;
                var startBringQuestComponent = ObjectActivationSystem.Instantiate(generalNpcConfigs.StartBringQuestPrefab, transform, transform.position);
                startBringQuestComponent.SetQuestConfigs(startBringQuestModifier.questConfigs);
            }
        }
    }
}
