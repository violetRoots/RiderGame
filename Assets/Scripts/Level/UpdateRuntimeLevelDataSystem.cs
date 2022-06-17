using UnityEngine;
using System.Collections;
using Leopotam.Ecs;
using RiderGame.SO;

namespace RiderGame.Level
{
    public class UpdateRuntimeLevelDataSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly GameConfiguration _configs;
        private readonly RuntimeLevelData _levelData;

        //private int _fixedStageIndex = 0;

        public void Init()
        {
            //    //todo заменить на выбор левела из интерфейса игры
            _levelData.CurrentLevelConfig = _configs.levelConfigs[0];

            //    SetNextStage();
        }

        public void Run()
        {
        //    var stage = _levelData.currentStage;

        //    var duration = stage.duration;
        //    ref var processTime = ref _levelData.processStageTime;

        //    if (processTime < duration)
        //    {
        //        var process = processTime / duration;

        //        _levelData.currentWorldSpeed = stage.ySpeedCurve.Evaluate(process) * stage.maxYSpeed;

        //        processTime += Time.deltaTime;
        //    }
        //    else
        //    {
        //        SetNextStage();
        //    }
        }

        //private void SetNextStage()
        //{
        //    ResetRuntimeStageValues();

        //    if(_fixedStageIndex < _levelData.currentLevelConfig.fixedStages.Length)
        //    {
        //        _levelData.currentStage = _levelData.currentLevelConfig.fixedStages[_fixedStageIndex];
        //        _fixedStageIndex++;
        //    }
        //    else
        //    {
        //        var randomStageIndex = Random.Range(0, _levelData.currentLevelConfig.randomStages.Length);
        //        _levelData.currentStage = _levelData.currentLevelConfig.randomStages[randomStageIndex];
        //    }
        //}

        //private void ResetRuntimeStageValues()
        //{
        //    _levelData.currentStage = null;
        //    _levelData.processStageTime = 0;
        //    _levelData.currentWorldSpeed = 0;
        //}
    }
}
