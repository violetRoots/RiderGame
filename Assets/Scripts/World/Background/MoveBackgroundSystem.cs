using UnityEngine;
using Leopotam.Ecs;
using RiderGame.RuntimeData;

namespace RiderGame.World
{
    public class MoveBackgroundSystem : IEcsRunSystem
    {
        private const float BackgroundSpeedMultiplier = 0.0004f;
        private const float BackgroundOffsetMultiplier = 10f;

        private readonly GameplayRuntimeData _levelData;

        private EcsFilter<MoveWorldObject, Background> _fBackground;
        private EcsFilter<MoveWorldObject, Background, MoveWorldObjectOffsetEvent> _fMoveWorldObjectEvent;

        public void Run()
        {
            foreach(var i in _fMoveWorldObjectEvent)
            {
                ref var moveComponent = ref _fMoveWorldObjectEvent.Get1(i);
                ref var background = ref _fMoveWorldObjectEvent.Get2(i);
                ref var eventData = ref _fMoveWorldObjectEvent.Get3(i);

                var offset = eventData.offset * BackgroundOffsetMultiplier;

                MoveBackground(ref background, offset);
            }

            foreach(var i in _fBackground)
            {
                ref var moveComponent = ref _fBackground.Get1(i);
                ref var background = ref _fBackground.Get2(i);

                if (moveComponent.moveOnUpdate)
                {
                    var offset = Quaternion.Euler(0, 0, _levelData.MovementDirection) * Vector2.up * _levelData.MovementSpeed;

                    MoveBackground(ref background, offset);
                }
            }
        }

        private void MoveBackground(ref Background background, Vector3 offset)
        {
            background.backgroundTextureOffset -= offset;
            background.renderer.material.mainTextureOffset = background.backgroundTextureOffset * BackgroundSpeedMultiplier;
        }
    }
}