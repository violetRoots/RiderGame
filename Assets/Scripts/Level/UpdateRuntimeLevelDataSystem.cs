using UnityEngine;
using Leopotam.Ecs;
using SkyCrush.WSGenerator;
using SkyCrush.Utility;
using RiderGame.SO;
using Input = RiderGame.Inputs.Input;

namespace RiderGame.Level
{
    public class UpdateRuntimeLevelDataSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private readonly GameConfiguration _gameConfigs;
        private readonly Generator _generator;
        private readonly RuntimeLevelData _levelData;

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

                var direction = _levelData.MovementDirection + (input.mouseDelta.x * _gameConfigs.DirectionSensitivity);
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
