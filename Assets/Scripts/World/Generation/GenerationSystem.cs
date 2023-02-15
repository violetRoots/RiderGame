using UnityEngine;
using UniRx;
using Leopotam.Ecs;
using SkyCrush.WSGenerator;
using RiderGame.RuntimeData;

namespace RiderGame.World
{
    public class GenerationSystem : IEcsInitSystem
    {
        private readonly Generator _generator;
        private readonly GameplayRuntimeData _gameplayRuntimeData;

        public void Init()
        {
            _gameplayRuntimeData.IsWorldMoving.Subscribe(OnIsMovingValueChanged);
        }

        private void OnIsMovingValueChanged(bool newValue)
        {
            if (newValue)
                _generator.Unpause();
            else
                _generator.Pause();
        }
    }
}
