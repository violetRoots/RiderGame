using System.Collections;
using UnityEngine;
using Leopotam.Ecs;
using RiderGame.RuntimeData;
using RiderGame.SO;

namespace RiderGame.World
{
    public class MoveBackgroundSystem : IEcsRunSystem
    {
        private readonly EcsStartup _ecsStartup;
        private readonly GameConfiguration _gameConfigs;
        private readonly GameplayRuntimeData _gameplayRuntimeData;

        private EcsFilter<MoveWorldObject, Background> _fBackground;
        private EcsFilter<MoveWorldObject, Background, MoveWorldObjectOffsetEvent> _fMoveBackgroundOffsetEvent;

        public void Run()
        {
            foreach(var i in _fMoveBackgroundOffsetEvent)
            {
                ref var moveComponent = ref _fMoveBackgroundOffsetEvent.Get1(i);
                ref var background = ref _fMoveBackgroundOffsetEvent.Get2(i);
                ref var eventData = ref _fMoveBackgroundOffsetEvent.Get3(i);

                MoveBackgroundOffset(ref background, eventData);
            }

            foreach(var i in _fBackground)
            {
                ref var moveComponent = ref _fBackground.Get1(i);
                ref var background = ref _fBackground.Get2(i);

                if (_gameplayRuntimeData.IsWorldMoving.Value)
                {
                    var offset = Quaternion.Euler(0, 0, _gameplayRuntimeData.MovementDirection) * Vector2.up * _gameplayRuntimeData.MovementSpeed * Time.deltaTime;
                    background.backgroundTextureOffset -= offset;
                    background.renderer.material.mainTextureOffset = background.backgroundTextureOffset * _gameConfigs.BackgroundSpeedMultiplier;
                }
            }
        }

        private void MoveBackgroundOffset(ref Background background, 
                                          MoveWorldObjectOffsetEvent eventData)
        {
            _ecsStartup.StopCoroutine(nameof(MoveBackgroundOffsetProcess));

            var startOffset = background.backgroundTextureOffset;
            background.backgroundTextureOffset -= eventData.offset;
            _ecsStartup.StartCoroutine(MoveBackgroundOffsetProcess(background.renderer, startOffset, background.backgroundTextureOffset, eventData));
        }

        private IEnumerator MoveBackgroundOffsetProcess(SpriteRenderer renderer, Vector3 startOffset, Vector3 endOffset, MoveWorldObjectOffsetEvent eventData)
        {
            var count = 0.0f;
            while(count <= eventData.time)
            {
                var currentOffset = Vector3.Lerp(startOffset, endOffset, count / eventData.time);
                renderer.material.mainTextureOffset = currentOffset * _gameConfigs.BackgroundSpeedMultiplier;

                yield return null;

                count += Time.deltaTime;
            }
        }
    }
}