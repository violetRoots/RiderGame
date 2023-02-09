using System.Collections;
using UnityEngine;
using Leopotam.Ecs;
using System;

namespace RiderGame.Gameplay
{
    public class BaseEffectSystem : IEcsRunSystem
    {
        public readonly EcsFilter<EffectsContainer> _fBaseEffects;

        public void Run()
        {
            foreach(var i in _fBaseEffects)
            {
                ref var entity = ref _fBaseEffects.GetEntity(i);
                ref var effectContainer = ref _fBaseEffects.Get1(i);

                for(var j = 0; j < effectContainer.effectsInfo.Count; j++)
                {
                    var effect = effectContainer.effectsInfo[j];

                    effect.process += Time.deltaTime;
                    if (effect.process >= effect.duration)
                    {
                        effect.onEndAction?.Invoke();
                        effect.disableAction?.Invoke();
                    }

                    effectContainer.effectsInfo[j] = effect;
                }

                effectContainer.effectsInfo.RemoveAll((effectInfo) => effectInfo.process >= effectInfo.duration);

                if (effectContainer.effectsInfo.Count == 0) 
                    entity.Del<EffectsContainer>();
            }
        }
    }
}
