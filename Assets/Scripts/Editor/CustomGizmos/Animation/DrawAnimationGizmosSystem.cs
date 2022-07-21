using Leopotam.Ecs;
using RiderGame.Gameplay;
using RiderGame.Level;
using RiderGame.World;
using UnityEngine;

namespace RiderGame.Editor.CustomGizmos
{
#if UNITY_EDITOR
    public class DrawAnimationGizmosSystem : IEcsRunSystem
    {
        private readonly RuntimeLevelData _runtimeLevelData;

        private readonly EcsFilter<EcsGameObject, Player, CustomGizmos> _playerFilter;

        public void Run()
        {
            foreach (var i in _playerFilter)
            {
                ref var gameObject = ref _playerFilter.Get1(i);
                ref var player = ref _playerFilter.Get2(i);
                ref var customGizmos = ref _playerFilter.Get3(i);

                ((CharacterAnimationDrawer)customGizmos.drawer).UpdateValue(player);

                var transform = gameObject.instance.transform;
                var endPos = Quaternion.Euler(0, 0, _runtimeLevelData.MovementDirection) * Vector3.down * CharacterAnimationDrawer.LineLength;

                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, transform.position + endPos);
            }
        }
    }
#endif
}
