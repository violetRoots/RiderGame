using UnityEngine;
using Leopotam.Ecs;
using RiderGame.Gameplay;

namespace RiderGame.World
{
    public class NpcOverlaySystem : IEcsRunSystem
    {
        private const string UnderPlayerLayer = "UnderPlayer";
        private const string AbovePlayerLayer = "AbovePlayer";

        private readonly EcsFilter<EcsGameObject, Npc, ActiveObject> _fNpc;

        public void Run()
        {
            foreach (var i in _fNpc)
            {
                ref var gameObject = ref _fNpc.Get1(i);
                ref var npc = ref _fNpc.Get2(i);

                var npcConfigs = npc.npcConfiguration;

                if (npcConfigs.IsDynamicOverlayModeOn)
                {
                    var layerEdgePos = gameObject.instance.transform.position.y + npcConfigs.DynamicOverlayEdgeOffset;
                    npc.spriteRenderer.sortingLayerName = layerEdgePos < 0 ? AbovePlayerLayer : UnderPlayerLayer;
                }
                else if (npcConfigs.IsStaticOverlayModeOn)
                {
                    npc.spriteRenderer.sortingLayerName = npcConfigs.StaticSortingLayer;
                }

                npc.spriteRenderer.sortingOrder = -(int)gameObject.instance.transform.position.y;
            }
        }
    }
}
