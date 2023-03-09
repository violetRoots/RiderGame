using UnityEngine;
using Leopotam.Ecs;
using RiderGame.RuntimeData;
using RiderGame.SO;
using RiderGame.World;
using Input = RiderGame.Inputs.Input;

namespace RiderGame.Gameplay
{
    public class PlayerDashSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly GameConfiguration _gameConfigs;
        private readonly GameplayRuntimeData _gameplayRuntimeData;

        private EcsFilter<Player> _fPlayer;
        private EcsFilter<Input> _fInput;

        private CharacterConfiguration _character;

        public void Init()
        {
            _character = _fPlayer.Get1(0).character;
        }

        public void Run()
        {
            foreach (var i in _fInput)
            {
                ref var input = ref _fInput.Get1(i);
                var playerConfigs = _gameConfigs.GeneralCharacterConfiguration;

                if (!input.tap.ended
                    || input.tap.endedTime - input.tap.startedTime >= playerConfigs.DashDetectTime
                    || Mathf.Abs(Time.time - _gameplayRuntimeData.DashLastUseTime) <= _character.DashCooldown) 
                        continue;

                PushPlayer();
                AddDashEffects();

                _gameplayRuntimeData.SetDashLastUseTime(Time.time);
            }
        }

        private void PushPlayer()
        {
            var offset = Quaternion.Euler(0, 0, _gameplayRuntimeData.MovementDirection) * Vector2.up * _character.DashDistance;
            var time = _character.DashDuration;

            MoveWorldObjectSystem.MoveWorldObjectByOffset(offset, time);
        }

        private void AddDashEffects()
        {
            foreach(var i in _fPlayer)
            {
                var entity = _fPlayer.GetEntity(i);
                ref var player = ref _fPlayer.Get1(i);

                var duration = _character.DashDuration;

                IgnoreMovementAnimationEffect.AddToEntity(entity, duration);
                InvulnerabilityEffect.AddToEntity(entity, duration);

                var dashAnimationInfo = player.character.DashAnimationConfigs.GetAnimationByAngle(_gameplayRuntimeData.MovementDirection);
                BaseAnimatorControllerSystem.AddAnimation(entity, dashAnimationInfo.animation, PlayerAnimationPriority.Dash);
            }
        }
    }
}
