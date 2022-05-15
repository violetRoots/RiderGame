using UnityEngine;
using Leopotam.Ecs;

namespace RiderGame.Inputs
{
    public class InputSystem : IEcsRunSystem
    {
        private EcsFilter<Input> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var input = ref filter.Get1(i);
#if UNITY_EDITOR
                input.horizontal = UnityEngine.Input.GetAxis("Horizontal");
#endif
            }
        }
    }
}