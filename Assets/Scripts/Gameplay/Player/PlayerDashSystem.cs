using UnityEngine;
using Leopotam.Ecs;
using RiderGame.RuntimeData;
using RiderGame.SO;
using RiderGame.World;
using Input = RiderGame.Inputs.Input;

namespace RiderGame.Gameplay
{
    public class PlayerDashSystem : IEcsRunSystem
    {
        private readonly GameConfiguration _gameConfigs;
        private readonly GameplayRuntimeData _gameplayRuntimeData;

        private EcsFilter<Input> _fInput;

        public void Run()
        {
            foreach (var i in _fInput)
            {
                ref var input = ref _fInput.Get1(i);
                var playerConfigs = _gameConfigs.PlayerConfiguration;

                if (!input.swipeDown || Mathf.Abs(Time.time - _gameplayRuntimeData.DashLastUseTime) <= playerConfigs.DashCooldown) continue;

                var offset = Quaternion.Euler(0, 0, _gameplayRuntimeData.MovementDirection) * Vector2.up * playerConfigs.DashDistance;
                var time = playerConfigs.DashTime;

                MoveWorldObjectSystem.MoveWorldObjectByOffset(offset, time);

                _gameplayRuntimeData.SetDashLastUseTime(Time.time);
            }
        }
    }
}
