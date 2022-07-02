using UnityEngine;
using Leopotam.Ecs;

namespace RiderGame.World
{
    public class ObjectOverlaySystem : IEcsRunSystem
    {
        private const string UnderPlayerLayer = "UnderPlayer";
        private const string AbovePlayerLayer = "AbovePlayer";

        private readonly EcsFilter<EcsGameObject, ActiveObject, Overlay> _filter;

        public void Run()
        {
            foreach(var i in _filter)
            {
                ref var gameObject = ref _filter.Get1(i);
                ref var overlay = ref _filter.Get3(i);

                var layerEdgePos = gameObject.instance.transform.position.y + overlay.layerEdgeOffset;
                overlay.spriteRenderer.sortingLayerName = layerEdgePos < 0 ? AbovePlayerLayer : UnderPlayerLayer;

                overlay.spriteRenderer.sortingOrder = -(int)gameObject.instance.transform.position.y;
            }
        }
    }
}
