using Leopotam.Ecs;
using RiderGame.World;

namespace RiderGame.Editor.CustomGizmos
{
#if UNITY_EDITOR
    public class DrawOverlayGizmosSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CustomGizmos, Overlay> _customGizmosFilter;

        public void Run()
        {
            foreach(var i in _customGizmosFilter)
            {
                ref var customGizmo = ref _customGizmosFilter.Get1(i);
                ref var overlay = ref _customGizmosFilter.Get2(i);

                ((ObjectLayerDrawer) customGizmo.drawer).UpdateValue(overlay);
            }
        }
    }
#endif
}
