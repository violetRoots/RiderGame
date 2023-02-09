using Leopotam.Ecs;
using RiderGame.Gameplay;
using RiderGame.RuntimeData;
using RiderGame.World;
using UnityEngine;

namespace RiderGame.Editor.CustomGizmos
{
#if UNITY_EDITOR
    public class DrawMovementAnimationGizmosSystem : IEcsRunSystem
    {
        private readonly GameplayRuntimeData _runtimeLevelData;

        private readonly EcsFilter<EcsGameObject, Movement, MovementAnimation, CustomGizmos> _animationFilter;

        public void Run()
        {
            foreach (var i in _animationFilter)
            {
                ref var gameObject = ref _animationFilter.Get1(i);
                ref var movement = ref _animationFilter.Get2(i);
                ref var animation = ref _animationFilter.Get3(i);
                ref var customGizmos = ref _animationFilter.Get4(i);

                if (!animation.drawGizmos) continue;

                var movementAnimationDrawer = customGizmos.drawer as MovementAnimationDrawer;
                movementAnimationDrawer?.UpdateValue(animation);

                var transform = gameObject.instance.transform;
                var endPos = Quaternion.Euler(0, 0, movement.DirectionAngle) * Vector3.down * MovementAnimationDrawer.LineLength;

                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, transform.position + endPos);
            }
        }
    }
#endif
}
