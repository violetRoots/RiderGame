using UnityEngine;
using Leopotam.Ecs;
using RiderGame.SO;
using RiderGame.RuntimeData;

namespace RiderGame.Gameplay
{
    public class LifesControlSystem : IEcsInitSystem
    {
        private readonly GameConfiguration _gameConfigs;

        private readonly SessionRuntimeData _sessionRuntimeData;

        public void Init()
        {
            var startLifesCount = _gameConfigs.GeneralCharacterConfiguration.MaxLifesCount;

            _sessionRuntimeData.LifesCount.Value = startLifesCount;
        }
    }
}
