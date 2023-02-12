using Leopotam.Ecs;
using RiderGame.SO;

namespace RiderGame.Gameplay
{
    public class ClampMovementSystem : IEcsRunSystem
    {
        private readonly GameConfiguration _gameCongfigs;

        private readonly EcsFilter<Movement> _fMovement;

        public void Run()
        {
            foreach(var i in _fMovement)
            {
                ref var movement = ref _fMovement.Get1(i);

                movement.clampAngle = _gameCongfigs.ClampDirectionAngle;
            }
        }
    }
}
