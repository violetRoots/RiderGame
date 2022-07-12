using Leopotam.Ecs;
using RiderGame.Gameplay;
using RiderGame.Level;
using RiderGame.World;
using UnityEngine;

namespace RiderGame.Editor.CustomGizmos
{
    public class DrawAnimationGizmosSystem : IEcsRunSystem
    {
        private const float LineLength = 10.0f;

        private readonly RuntimeLevelData _runtimeLevelData;

        private readonly EcsFilter<CustomGizmos, EcsGameObject, Player> _playerFilter;

        public void Run()
        {
            foreach(var i in _playerFilter)
            {
                ref var gameObject = ref _playerFilter.Get2(i);

                var transform = gameObject.instance.transform;
                var endPos = Quaternion.Euler(0, 0, _runtimeLevelData.MovementDirection) * Vector3.down * LineLength;

                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, transform.position + endPos);
            }
        }
    }
}
