using System;
using System.Collections.Generic;
using Leopotam.Ecs;

namespace RiderGame.Gameplay
{
    public static class BaseEffect
    {
        public static void AddEffect<T>(EcsEntity entity, T effect, float duration, Action onEndAction) where T : struct
        {
            if (TryGetEffectsContainer(entity, out EffectsContainer effectsContainer))
            {
                effectsContainer.effectsInfo.RemoveAll((info) => info.effectType == typeof(T));
            }
            else
            {
                entity.Replace(effectsContainer);
            }

            var baseEffectInfo = new BaseEffectInfo()
            {
                effectType = typeof(T),
                duration = duration,
                onEndAction = onEndAction,
                disableAction = () => entity.Del<T>()
            };

            entity.Replace(effect);
            effectsContainer.effectsInfo.Add(baseEffectInfo);
        }

        public static void RemoveEffect<T>(EcsEntity entity) where T : struct
        {
            if (!TryGetEffectsContainer(entity, out EffectsContainer effectsContainer)) return;

            effectsContainer.effectsInfo.RemoveAll((info) => info.effectType == typeof(T));
            entity.Del<T>();
        }

        private static bool TryGetEffectsContainer(EcsEntity entity, out EffectsContainer effectsContainer)
        {
            if (entity.Has<EffectsContainer>())
            {
                effectsContainer = entity.Get<EffectsContainer>();
                return true;
            }
            else
            {
                effectsContainer = new EffectsContainer()
                {
                    effectsInfo = new List<BaseEffectInfo>()
                };

                entity.Replace(effectsContainer);
                return false;
            }
        }
    }

    public struct EffectsContainer
    {
        public List<BaseEffectInfo> effectsInfo;
    }

    public struct BaseEffectInfo
    {
        public Type effectType;
        public float duration;
        public float process;
        public Action onEndAction;
        public Action disableAction;
    }
}
