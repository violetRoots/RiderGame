using UnityEngine;
using Leopotam.Ecs;

namespace RiderGame.Gameplay
{
    public static class IgnoreMovementAnimationEffect
    {
        public static void AddToEntity(EcsEntity entity, float duration)
        {
            BaseEffect.AddEffect(entity, new IgnoreMovementAnimation(), duration, null);
        }
    }

    public struct IgnoreMovementAnimation { }
}
