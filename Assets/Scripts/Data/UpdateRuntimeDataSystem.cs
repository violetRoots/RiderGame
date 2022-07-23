using UnityEngine;
using UnityEditor;
using Leopotam.Ecs;
using SkyCrush.WSGenerator;
using SkyCrush.Utility;
using RiderGame.SO;
using Input = RiderGame.Inputs.Input;

namespace RiderGame.RuntimeData
{
    public class UpdateRuntimeDataSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private readonly GameConfiguration _gameConfigs;
        private readonly Generator _generator;
        private readonly GameplayRuntimeData _levelData;

        private readonly EcsFilter<Input> _inputFilter;

        public void Init()
        {
            _levelData.Init(_gameConfigs.Levels[0]);

            _generator.StageManager.OnStartStage += UpdateMovementSpeed;
        }

        public void Run()
        {
            foreach (var i in _inputFilter)
            {
                ref var input = ref _inputFilter.Get1(i);

                var direction = _levelData.MovementDirection;
#if UNITY_EDITOR
                if (EditorApplication.isRemoteConnected)
                {
                    direction += input.mouseDelta.x * _gameConfigs.TouchDirectionSensitivity;
                }
                else
                {
                    direction += input.mouseDelta.x * _gameConfigs.MouseDirectionSensitivity;
                }
#elif UNITY_ANDROID || UNITY_IOS
                direction += input.mouseDelta.x * _gameConfigs.TouchDirectionSensitivity;
#endif
                direction = CustomMath.ClampAngle(direction, -_gameConfigs.ClampDirectionAngle, _gameConfigs.ClampDirectionAngle);
                _levelData.SetMovementDirection(direction);
            }
        }

        public void Destroy()
        {
            _generator.StageManager.OnStartStage -= UpdateMovementSpeed;
        }

        private void UpdateMovementSpeed(Stage stage)
        {
            _levelData.SetMovementSpeed(stage.CustomData.MovementSpeed);
        }
    }
}
