using UnityEngine;
using Leopotam.Ecs;
using RiderGame.SO;
using RiderGame.World;

namespace RiderGame.Gameplay
{
    public class NpcWalkStateSystem : IEcsRunSystem
    {
        private readonly GameConfiguration _gameConfigs;

        private readonly EcsFilter<EcsGameObject, Npc, ActiveObject> _fNpc;
        private readonly EcsFilter<EcsGameObject, Npc, ActivationEvent> _fNpcActivationEvent;

        public void Run()
        {
            foreach(var i in _fNpcActivationEvent)
            {
                ref var npc = ref _fNpcActivationEvent.Get2(i);

                if (!npc.StateController.TryGet(out WalkState walkState)) continue;

                var clampValue = GameConfiguration.ClampDirectionAngle;
                walkState.DirectionAngle = Random.Range(-clampValue, clampValue);
            }

            foreach(var i in _fNpc)
            {
                var entity = _fNpc.GetEntity(i);

                ref var gameObject = ref _fNpc.Get1(i);
                ref var npc = ref _fNpc.Get2(i);

                var stateController = npc.StateController;

                if (!stateController.TryGetActiveStateAs(out WalkState walkState)) continue;

                var transform = gameObject.instance.transform;
                var translation = Quaternion.Euler(0, 0, walkState.DirectionAngle) * Vector2.down * walkState.MovementSpeed;

                transform.Translate(translation * Time.deltaTime);

                if (walkState.MovementAnimation == null) continue;

                var currentAnimationInfo = walkState.MovementAnimation.GetAnimationByAngle(walkState.DirectionAngle);

                short flipValue = (short)(currentAnimationInfo.isFlip ? -1 : 1);
                BaseAnimatorControllerSystem.AddAnimation(entity, currentAnimationInfo.animation, CharacterAnimationPriority.Movement, loop: true, continueFrame: true, flipDir: flipValue);
            }
        }
    }
}
