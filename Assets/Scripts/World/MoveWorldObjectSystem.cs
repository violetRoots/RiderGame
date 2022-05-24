using UnityEngine;
using Leopotam.Ecs;
using RiderGame.Level;

namespace RiderGame.World
{
    public class MoveWorldObjectSystem : IEcsRunSystem
    {
        private RuntimeLevelData _levelData;

        private EcsFilter<WorldObject> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var moveObject = ref _filter.Get1(i);

                moveObject.rigidbody.velocity = Vector2.up * _levelData.currentWorldSpeed * Time.deltaTime;
            }
        }
    }

}