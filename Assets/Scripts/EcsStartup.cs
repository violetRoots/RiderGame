using UnityEngine;
using Leopotam.Ecs;
using Voody.UniLeo;
using LeoEcsPhysics;
using SkyCrush.WSGenerator;
using RiderGame.Inputs;
using RiderGame.SO;
using RiderGame.World;
using RiderGame.RuntimeData;
using RiderGame.Physics;
using RiderGame.Gameplay;
using RiderGame.Editor.CustomGizmos;

namespace RiderGame
{
    public class EcsStartup : MonoBehaviour
    {
        [SerializeField] private SessionStartup sessionStartup;
        [SerializeField] private GameConfiguration gameConfigs;
        [SerializeField] private Generator generator;

        private EcsStartup _ecsStartupObject;
        private EcsWorld _ecsWorld;
        private EcsSystems _updateSystems;
        private EcsSystems _gizmosSystems;

        private void Awake()
        {
            _ecsStartupObject = this;

            _ecsWorld = new EcsWorld();
            _updateSystems = new EcsSystems(_ecsWorld);

            EcsPhysicsEvents.ecsWorld = _ecsWorld;

            _updateSystems
                .ConvertScene()
                .Inject(_ecsStartupObject)
                .Inject(gameConfigs)
                .Inject(generator)
                .Inject(sessionStartup.GameplayRuntimeData)
                .Inject(sessionStartup.SessionRuntimeData)

                //Physics
                .Add(new OneFramePhysicsSystem())
                .Add(new ObstacleCollisionSystem())
                .Add(new EnemyCollisionSystem())

                //Runtime data updating
                .Add(new UpdateRuntimeDataSystem())

                //Input
                .Add(new InputSystem())

                //World
                .Add(new ObjectActivationSystem())
                .Add(new ObjectOverlaySystem())
                .Add(new MoveWorldObjectSystem())
                .Add(new MoveBackgroundSystem())

                //Player
                .Add(new PlayerAnimationSystem())
                .Add(new BaseEffectSystem())
                .Add(new InvulnerabilitySystem())
                .Add(new CoinCollectionSystem())

                //Enemy
                .Add(new EnemyMovementSystem())
                .Add(new EnemyAnimationSystem())

                //Animation
                .Add(new MovementAnimationSystem())

                .Init();
#if UNITY_EDITOR
            _gizmosSystems = new EcsSystems(_ecsWorld);
            _gizmosSystems
                .Inject(_ecsStartupObject)
                .Inject(gameConfigs)
                .Inject(generator)
                .Inject(sessionStartup.GameplayRuntimeData)

                .Add(new DrawMovementAnimationGizmosSystem())
                .Add(new DrawOverlayGizmosSystem())
                .Init();
#endif
        }

        private void Update()
        {
            _updateSystems?.Run();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {

            if (!Application.isPlaying) return;

            _gizmosSystems.Run();
        }
#endif

        private void OnDestroy()
        {
            EcsPhysicsEvents.ecsWorld = null;
            _gizmosSystems?.Destroy();
            _gizmosSystems = null;
            _updateSystems?.Destroy();
            _updateSystems = null;
            _ecsWorld?.Destroy();
            _ecsWorld = null;
        }
    }
}
