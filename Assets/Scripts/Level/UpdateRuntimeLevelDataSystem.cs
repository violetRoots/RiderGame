using UnityEngine;
using System.Collections;
using Leopotam.Ecs;
using RiderGame.SO;

namespace RiderGame.Level
{
    public class UpdateRuntimeLevelDataSystem : IEcsRunSystem, IEcsInitSystem
    {
        private GameConfiguration _configs;
        private RuntimeLevelData _levelData;

        private int _fixedStageIndex = 0;

        public void Init()
        {
            SetNextStage();
        }

        public void Run()
        {
            var stage = _levelData.currentStage;

            var duration = stage.duration;
            ref var processTime = ref _levelData.processStageTime;

            if (processTime < duration)
            {
                var process = processTime / duration;

                _levelData.currentWorldSpeed = stage.speedCurve.Evaluate(process) * stage.maxSpeed;

                processTime += Time.deltaTime;
            }
            else
            {
                SetNextStage();
            }
        }

        private void SetNextStage()
        {
            ResetRuntimeStageValues();

            if(_fixedStageIndex < _configs.Level.fixedStages.Length)
            {
                _levelData.currentStage = _configs.Level.fixedStages[_fixedStageIndex];
                _fixedStageIndex++;
            }
            else
            {
                var randomStageIndex = Random.Range(0, _configs.Level.randomStages.Length);
                _levelData.currentStage = _configs.Level.randomStages[randomStageIndex];
            }
        }

        private void ResetRuntimeStageValues()
        {
            _levelData.currentStage = null;
            _levelData.processStageTime = 0;
            _levelData.currentWorldSpeed = 0;
        }
    }
}
