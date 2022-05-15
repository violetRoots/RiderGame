using UnityEngine;
using Leopotam.Ecs;
using Input = RiderGame.Inputs.Input;

namespace RiderGame.Player
{
    public class PlayerMovementSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerMovement, Input> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var playerMovement = ref filter.Get1(i);
                ref var input = ref filter.Get2(i);

                playerMovement.rigidbody.velocity = new Vector2(input.horizontal, -1) * playerMovement.speed * Time.deltaTime;
            }
        }
    }
}
