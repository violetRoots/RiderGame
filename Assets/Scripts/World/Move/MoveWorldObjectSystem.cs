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
        private readonly GameplayRuntimeData _levelData;

        private readonly static List<GameObject> WorldObjects = new List<GameObject>();

        private EcsFilter<Input, EcsGameObject, MoveWorldObject> _fWorldObject;
        private EcsFilter<EcsGameObject, MoveWorldObject, MoveWorldObjectOffsetEvent> _fMoveWorldOjectEvent;

        public static void MoveWorldObjectByOffset(Vector3 offset, float movementTime, Ease ease = Ease.Linear)
        {
            foreach (var gameObject in WorldObjects)
            {
                var moveEvent = new MoveWorldObjectOffsetEvent()
                {
                    offset = offset,
                    time = movementTime,
                    ease = ease
                };

                var entity = gameObject.GetComponent<ConvertToEntity>().TryGetEntity();
                OneFrameEventSystem.AddOneFrameEvent(entity.Value, moveEvent);
            }
        }

        public void Init()
        {
            WorldObjects.Clear();
            foreach (var i in _fWorldObject)
            {
                ref var gameObject = ref _fWorldObject.Get2(i);

                WorldObjects.Add(gameObject.instance);
            }
        }

        public void Run()
        {
            foreach(var i in _fMoveWorldOjectEvent)
            {
                ref var entity = ref _fMoveWorldOjectEvent.GetEntity(i);

                ref var gameObject = ref _fMoveWorldOjectEvent.Get1(i);
                ref var moveComponent = ref _fMoveWorldOjectEvent.Get2(i);
                ref var eventData = ref _fMoveWorldOjectEvent.Get3(i);

                MoveByOffset(gameObject.instance, ref moveComponent, ref eventData);
            }

            foreach (var i in _fWorldObject)
            {
                ref var input = ref _fWorldObject.Get1(i);
                ref var gameObject = ref _fWorldObject.Get2(i);
                ref var moveComponent = ref _fWorldObject.Get3(i);

                if (!moveComponent.moveOnUpdate) continue;

                var translation = Quaternion.Euler(0, 0, _levelData.MovementDirection) * new Vector3(0, _levelData.MovementSpeed, 0) * Time.deltaTime;
                gameObject.instance.transform.Translate(translation);
            }
        }

        private void MoveByOffset(GameObject  gameObject, 
                                  ref MoveWorldObject moveComponent, 
                                  ref MoveWorldObjectOffsetEvent eventData)
        {
            moveComponent.moveOnUpdate = false;

            gameObject.transform.DOMove(gameObject.transform.position + eventData.offset, eventData.time)
                                .SetEase(eventData.ease)
                                .OnComplete(() => SetMoveOnUpdateValue(gameObject, true));
        }

        private void SetMoveOnUpdateValue(GameObject target, bool value)
        {
            foreach (var i in _fWorldObject)
            {
                ref var gameObject = ref _fWorldObject.Get2(i);
                ref var moveComponent = ref _fWorldObject.Get3(i);

                if (gameObject.instance != target) continue;

                moveComponent.moveOnUpdate = value;
            }
        }
    }
}