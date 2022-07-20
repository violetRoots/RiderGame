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

        private readonly EcsFilter<EcsGameObject, Player> _playerFilter;
        private readonly EcsFilter<CustomGizmos, CharacterAnimation> _animationFilter;

        public void Run()
        {
            foreach (var i in _animationFilter)
            {
                ref var customGizmos = ref _animationFilter.Get1(i);
                ref var animation = ref _animationFilter.Get2(i);

                ((CharacterAnimationDrawer)customGizmos.drawer).UpdateValue(animation);
            }

            foreach (var i in _playerFilter)
            {
                ref var gameObject = ref _playerFilter.Get1(i);

                var transform = gameObject.instance.transform;
                var endPos = Quaternion.Euler(0, 0, _runtimeLevelData.MovementDirection) * Vector3.down * CharacterAnimationDrawer.LineLength;

                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, transform.position + endPos);
            }
        }
    }
#endif
}
