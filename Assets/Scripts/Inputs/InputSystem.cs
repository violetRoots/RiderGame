using UnityEngine;
using Leopotam.Ecs;

namespace RiderGame.Inputs
{
    public class InputSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Input> _filter;

        private Vector2 _previousMousePos;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var input = ref _filter.Get1(i);

                if (UnityEngine.Input.GetMouseButtonDown(0))
                {
                    _previousMousePos = UnityEngine.Input.mousePosition;
                }
                else if(UnityEngine.Input.GetMouseButton(0))
                {
                    Vector2 mousePos = UnityEngine.Input.mousePosition;

                    input.mouseDelta = mousePos - _previousMousePos;

                    _previousMousePos = mousePos;
                }
            }
        }
    }
}