using UnityEngine;
using Leopotam.Ecs;
using RiderGame.RuntimeData;

namespace RiderGame.Gameplay
{
    public class PlayerAnimationSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly GameplayRuntimeData _gameplayData;

        private readonly EcsFilter<Player, Movement, MovementAnimation>.Exclude<IgnoreMovementAnimation> _fPlayer;

        public void Init()
        {
            foreach (var i in _fPlayer)
            {
                ref var player = ref _fPlayer.Get1(i);
                ref var animationComponent = ref _fPlayer.Get3(i);

                if (!animationComponent.setCustomAnimations)
                {
                    animationComponent.animationConfiguration = player.character.WalkAnimationConfigs;
                }
            }
        }

        public void Run()
        {
            foreach(var i in _fPlayer)
            {
                ref var movement = ref _fPlayer.Get2(i);

                movement.DirectionAngle = _gameplayData.MovementDirection;
            }
        }
    }
}
