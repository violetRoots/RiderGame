using UnityEngine;
using Leopotam.Ecs;
using SkyCrush.WSGenerator;
using RiderGame.SO;

namespace RiderGame.Level
{
    public class UpdateRuntimeLevelDataSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly GameConfiguration _gameConfigs;
        private readonly Generator _generator;
        private readonly RuntimeLevelData _levelData;

        public void Init()
        {
            _levelData.Init(_gameConfigs.Levels[0]);

            _generator.StageManager.OnStartStage += UpdateYSpeed;
        }

        public void Destroy()
        {
            _generator.StageManager.OnStartStage -= UpdateYSpeed;
        }

        private void UpdateYSpeed(Stage stage)
        {
            _levelData.SetYSpeed(stage.CustomData.YSpeed);
        }
    }
}
