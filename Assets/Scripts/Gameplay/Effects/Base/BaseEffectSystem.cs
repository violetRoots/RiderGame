using System.Collections;
using UnityEngine;
using Leopotam.Ecs;

namespace RiderGame.Gameplay
{
    public class BaseEffectSystem : IEcsRunSystem
    {
        public readonly EcsFilter<BaseEffect> _fBaseEffects;

        public void Run()
        {
            foreach(var i in _fBaseEffects)
            {
                var entity = _fBaseEffects.GetEntity(i);
                ref var effect = ref _fBaseEffects.Get1(i);

                if (effect.isEnd) entity.Del<BaseEffect>();

                effect.process += Time.deltaTime;
                if(effect.process >= effect.duration)
                {
                    effect.isEnd = true;
                }
            }
        }
    }
}
