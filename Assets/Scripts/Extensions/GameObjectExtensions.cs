using UnityEngine;
using Leopotam.Ecs;
using RiderGame.World;

namespace RiderGame
{
    public static class GameObjectExtensions
    {
        public static bool FindActiveEntityWithComponent<T>(this GameObject gameObject) where T : struct
        {
            var activeEntities = ObjectActivationSystem.ActiveObjectEntities;

            foreach (var entity in activeEntities)
            {
                ref var gameObjectComponent = ref entity.Get<EcsGameObject>();

                if (gameObjectComponent.instance != gameObject || !entity.Has<T>()) continue;

                return true;
            }

            return false;
        }

        public static bool FindActiveEntityWithComponent<T>(this GameObject gameObject, out EcsEntity resEntity) where T : struct
        {
            var activeEntities = ObjectActivationSystem.ActiveObjectEntities;

            foreach(var entity in activeEntities)
            {
                ref var gameObjectComponent = ref entity.Get<EcsGameObject>();

                if (gameObjectComponent.instance != gameObject || !entity.Has<T>()) continue;

                resEntity = entity;
                return true;
            }

            resEntity = default;
            return false;
        }

        public static bool FindActiveEntityWithComponent<T>(this GameObject gameObject, out T component) where T : struct
        {
            var activeEntities = ObjectActivationSystem.ActiveObjectEntities;

            foreach (var entity in activeEntities)
            {
                ref var gameObjectComponent = ref entity.Get<EcsGameObject>();

                if (gameObjectComponent.instance != gameObject || !entity.Has<T>()) continue;

                component = entity.Get<T>();
                return true;
            }

            component = default;
            return false;
        }
    }
}
