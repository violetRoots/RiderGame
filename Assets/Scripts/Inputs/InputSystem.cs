using UnityEngine;
using Leopotam.Ecs;
using RiderGame.SO;

namespace RiderGame.Inputs
{
    public class InputSystem : IEcsRunSystem
    {
        private readonly GameConfiguration _gameConfigs;

        private readonly EcsFilter<Input> _filter;

        private Vector2 _previousMousePos;
        private Vector2 _mouseDelta;

        private Vector2 _startSwipeMousePos;
        private Vector2 _swipeOffset;
        private float _swipeDetectTime;
        private bool _swipeDown;

        public void Run()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                _previousMousePos = UnityEngine.Input.mousePosition;
                _startSwipeMousePos = _previousMousePos;
            }
            else if (UnityEngine.Input.GetMouseButton(0))
            {
                Vector2 mousePos = UnityEngine.Input.mousePosition;

                _mouseDelta = mousePos - _previousMousePos;

                _previousMousePos = mousePos;

                if (Mathf.Abs(_swipeDetectTime - Time.deltaTime) > _gameConfigs.SwipeDetectTime)
                {
                    _swipeDetectTime = Time.deltaTime;
                    _startSwipeMousePos = mousePos;
                }

                _swipeOffset = mousePos - _startSwipeMousePos;
                _swipeDown = _swipeOffset.y < 0 && Mathf.Abs(_swipeOffset.y / Screen.height) >= 1 - _gameConfigs.SwipeSensitivity;

                if (_swipeDown)
                {
                    _startSwipeMousePos = mousePos;
                }
            }
            else if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                _mouseDelta = Vector2.zero;
                _swipeOffset = Vector2.zero;
                _swipeDown = false;
            }

            foreach (var i in _filter)
            {
                ref var input = ref _filter.Get1(i);

                input.mouseDelta = _mouseDelta;
                input.swipeDown = _swipeDown;
            }
        }
    }
}