using UnityEngine;
using Leopotam.Ecs;
using RiderGame.SO;
using Input = RiderGame.Inputs.Input;

namespace RiderGame.Player
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
        private GameConfiguration _configs;

        private EcsFilter<PlayerMovement, Input> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var playerMovement = ref _filter.Get1(i);
                ref var input = ref _filter.Get2(i);

                playerMovement.rigidbody.velocity = new Vector2(input.horizontal, -1) * _configs.Player.baseSpeed * Time.deltaTime;
            }
        }
    }
}
