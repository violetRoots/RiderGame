using System;
using System.Linq;
using UnityEngine;
using UniRx;
using Leopotam.Ecs;
using RiderGame.World;
using RiderGame.SO;

namespace RiderGame.Gameplay
{
    public class NpcAggressionStateSystem : IEcsInitSystem, IEcsRunSystem
    {
        private const int RaysCount = 9;

        private readonly EcsFilter<EcsGameObject, Player> _fPlayer;
        private readonly EcsFilter<EcsGameObject, Npc, ActiveObject> _fNpc;
        private readonly EcsFilter<EcsGameObject, Npc, ActivationEvent> _fNpcActivationEvent;
        private readonly EcsFilter<EcsGameObject, Npc, DeactivationEvent> _fNpcDeactivationEvent;

        private GameObject _playerObject;

        public void Init()
        {
            _playerObject = _fPlayer.Get1(0).instance;
        }

        public void Run()
        {
            foreach (var i in _fNpcActivationEvent)
            {
                var npc = _fNpcActivationEvent.Get2(i);

                if (!npc.StateController.TryGet(out AggressionState aggressionState)) continue;

                aggressionState.changeStateSubscription = npc.StateController.ActiveState.Subscribe((state) => OnStateChanged(npc, state));
            }

            foreach (var i in _fNpc)
            {
                var entity = _fNpc.GetEntity(i);

                ref var gameObject = ref _fNpc.Get1(i);
                ref var npc = ref _fNpc.Get2(i);

                var stateController = npc.StateController;

                if (stateController.TryGetActiveStateAs(out WalkState activeWalkState) && stateController.TryGet(out AggressionState attachedAggressionState))
                {
                    if (attachedAggressionState.startAggressionRadius == 0.0f) continue;

                    var raycastOrigin = gameObject.instance.transform.position;
                    var angleStep = ((float)(GameConfiguration.ClampDirectionAngle * 2) / (RaysCount - 1));

                    for (var rayIndex = 0; rayIndex < RaysCount; rayIndex++)
                    {
                        var angle = GameConfiguration.ClampDirectionAngle - rayIndex * angleStep;
                        var raycastDirection = Quaternion.Euler(0, 0, angle) * Vector2.down;
                        var hits = Physics2D.RaycastAll(raycastOrigin, raycastDirection, attachedAggressionState.startAggressionRadius);

                        if (hits.Count((hit) => hit.collider.gameObject == _playerObject) == 0) continue;

                        stateController.TrySetActiveStateAs<AggressionState>();
                        break;
                    }
                }
                else if(stateController.TryGetActiveStateAs(out AggressionState activeAggressionState) && stateController.Has<WalkState>())
                {
                    var toPlayer = _playerObject.transform.position - gameObject.instance.transform.position;
                    var movementAngle = Vector2.SignedAngle(Vector2.down, toPlayer);
                    activeAggressionState.DirectionAngle = movementAngle;

                    var currentAnimationInfo = activeAggressionState.MovementAnimation.GetAnimationByAngle(activeAggressionState.DirectionAngle);
                    BaseAnimatorControllerSystem.AddAnimation(entity, currentAnimationInfo.animation, NpcAnimationPriority.Walk, loop: true, continueFrame: true, flipDir: currentAnimationInfo.FlipValue);

                    if (toPlayer.magnitude > activeAggressionState.endAggressionRadius)
                        stateController.TrySetActiveStateAs<WalkState>();
                }
            }

            foreach (var i in _fNpcDeactivationEvent)
            {
                var npc = _fNpcActivationEvent.Get2(i);

                if (!npc.StateController.TryGet(out AggressionState aggressionState)) continue;

                aggressionState.changeStateSubscription?.Dispose();
                aggressionState.changeStateSubscription = null;
            }
        }

        public void OnStateChanged(Npc npc, State newState)
        {
            var isAggressionActive = newState is AggressionState;

            npc.aggressionStateRefs.icon.gameObject.SetActive(isAggressionActive);
        }

        public static void SetStunnedState(ref Npc enemy)
        {
            //enemy.state = EnemyState.Stunned;

            //enemy.aggressionState.icon.gameObject.SetActive(false);
            //enemy.stunnedState.icon.gameObject.SetActive(true);
        }
    }
}
