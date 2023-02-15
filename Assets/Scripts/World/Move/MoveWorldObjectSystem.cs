using UnityEngine;
using Leopotam.Ecs;
using DG.Tweening;
using RiderGame.RuntimeData;
using Input = RiderGame.Inputs.Input;
using System.Collections.Generic;
using Voody.UniLeo;

namespace RiderGame.World
{
    public class MoveWorldObjectSystem : IEcsInitSystem, IEcsRunSystem
    {
        private static GameObject WorldObject;

        private readonly GameplayRuntimeData _gameplayRuntimeData;

        private EcsFilter<Input, EcsGameObject, MoveWorldObject> _fWorldObject;
        private EcsFilter<EcsGameObject, MoveWorldObject, MoveWorldObjectOffsetEvent> _fMoveWorldOjectEvent;

        public static void MoveWorldObjectByOffset(Vector3 offset, float movementTime, Ease ease = Ease.Linear)
        {
            var moveEvent = new MoveWorldObjectOffsetEvent()
            {
                offset = offset,
                time = movementTime,
                ease = ease
            };

            var entity = WorldObject.GetComponent<ConvertToEntity>().TryGetEntity();
            OneFrameEventSystem.AddOneFrameEvent(entity.Value, moveEvent);
        }

        public void Init()
        {
            WorldObject = _fWorldObject.Get2(0).instance;

            _gameplayRuntimeData.SetWorldIsMovingValue(true);
        }

        public void Run()
        {
            foreach(var i in _fMoveWorldOjectEvent)
            {
                ref var entity = ref _fMoveWorldOjectEvent.GetEntity(i);

                ref var gameObject = ref _fMoveWorldOjectEvent.Get1(i);
                ref var eventData = ref _fMoveWorldOjectEvent.Get3(i);

                MoveByOffset(gameObject.instance, ref eventData);
            }

            foreach (var i in _fWorldObject)
            {
                ref var input = ref _fWorldObject.Get1(i);
                ref var gameObject = ref _fWorldObject.Get2(i);
                ref var moveComponent = ref _fWorldObject.Get3(i);

                if (!_gameplayRuntimeData.IsWorldMoving.Value) continue;

                var translation = Quaternion.Euler(0, 0, _gameplayRuntimeData.MovementDirection) * new Vector3(0, _gameplayRuntimeData.MovementSpeed, 0) * Time.deltaTime;
                gameObject.instance.transform.Translate(translation);
            }
        }

        private void MoveByOffset(GameObject  gameObject, 
                                  ref MoveWorldObjectOffsetEvent eventData)
        {
            _gameplayRuntimeData.SetWorldIsMovingValue(false);

            gameObject.transform.DOMove(gameObject.transform.position + eventData.offset, eventData.time)
                                .SetEase(eventData.ease)
                                .OnComplete(() => _gameplayRuntimeData.SetWorldIsMovingValue(true));
        }
    }
}