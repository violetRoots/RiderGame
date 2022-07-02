using UnityEngine;
using Leopotam.Ecs;
using RiderGame.Level;
using Input = RiderGame.Inputs.Input;

namespace RiderGame.World
{
    public class MoveWorldObjectSystem : IEcsRunSystem
    {
        private readonly RuntimeLevelData _levelData;

        private EcsFilter<Input, EcsGameObject, MoveWorldObject> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var input = ref _filter.Get1(i);
                ref var gameObject = ref _filter.Get2(i);

                gameObject.instance.transform.Translate(new Vector2(-input.horizontal * _levelData.XSpeed, 
                    _levelData.YSpeed) * Time.deltaTime);
            }
        }
    }

}