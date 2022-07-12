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

                var translation = Quaternion.Euler(0, 0, _levelData.MovementDirection) * new Vector3(0, _levelData.MovementSpeed, 0) * Time.deltaTime;
                gameObject.instance.transform.Translate(translation);
            }
        }
    }

}