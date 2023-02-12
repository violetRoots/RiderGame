using UnityEngine;
using Leopotam.Ecs;
using RiderGame.SO;

namespace RiderGame.Inputs
{
    public class InputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private static readonly 
            float ScreenDiagonal = new Vector2(Screen.width, Screen.height).magnitude;

        private readonly GameConfiguration _gameConfigs;

        private readonly EcsFilter<Input> _filter;

        private Vector2 _startMousePos;
        private Vector2 _previousMousePos;
        private Vector2 _mouseDelta;

        private Vector2 _startSwipeMousePos;
        private Vector2 _swipeOffset;
        private float _swipeDetectTime;
        private bool _swipeDown;

        private Input.TapInfo _tapInfo;

        public void Init()
        {
            _tapInfo = new Input.TapInfo();
        }

        public void Run()
        {
            Vector2 mousePos = UnityEngine.Input.mousePosition;

            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                _tapInfo.started = true;
                _tapInfo.startedTime = Time.time;

                _startMousePos = mousePos;
                _previousMousePos = mousePos;
                _startSwipeMousePos = mousePos;
            }
            else if (UnityEngine.Input.GetMouseButton(0))
            {
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
                    _startSwipeMousePos = mousePos;
            }
            else if (UnityEngine.Input.GetMouseButtonUp(0))
            {
                _tapInfo.ended = Mathf.Abs(Time.time - _tapInfo.startedTime) >= _gameConfigs.TapDetectTime
                                 && (mousePos - _startMousePos).magnitude <= _gameConfigs.MaxTapOffset * ScreenDiagonal;
                if (_tapInfo.ended)
                    _tapInfo.endedTime = Time.time;

                _mouseDelta = Vector2.zero;
                _swipeOffset = Vector2.zero;
            }

            foreach (var i in _filter)
            {
                ref var input = ref _filter.Get1(i);

                input.tap = _tapInfo;
                input.mouseDelta = _mouseDelta;
                input.swipeDown = _swipeDown;
            }

            _tapInfo.started = false;
            _tapInfo.ended = false;
            _swipeDown = false;
        }
    }
}