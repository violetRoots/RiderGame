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
using RiderGame.UI;

namespace RiderGame
{
    public class EcsStartup : MonoBehaviour
    {
        [SerializeField] private SessionStartup sessionStartup;
        [SerializeField] private GameConfiguration gameConfigs;
        [SerializeField] private Generator generator;

        private EcsStartup _ecsStartupObject;
        private EcsWorld _ecsWorld;
        private EcsSystems _UISystems;
        private EcsSystems _gameplaySystems;
        private EcsSystems _gizmosSystems;

        private void Awake()
        {
            _ecsStartupObject = this;

            _ecsWorld = new EcsWorld();
            _UISystems = new EcsSystems(_ecsWorld);
            _gameplaySystems = new EcsSystems(_ecsWorld);

            EcsPhysicsEvents.ecsWorld = _ecsWorld;

            _UISystems
                .ConvertScene()
                .Inject(sessionStartup.SessionRuntimeData)

                //UI
                .Add(new UpperPanelSystem())
                .Add(new EndSessionPanelSystem())

                .Init();


            _gameplaySystems
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
                .Add(new UpdateSessionDataSystem())
                .Add(new UpdateGameplayDataSystem())

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
                .Add(new EnemyStateSystem())
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
            _UISystems?.Run();

            if(sessionStartup.SessionRuntimeData != null &&
                sessionStartup.SessionRuntimeData.Status.Value == SessionStatus.Playing) 
                _gameplaySystems?.Run();
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
            _gameplaySystems?.Destroy();
            _gameplaySystems = null;
            _UISystems?.Destroy();
            _UISystems = null;
            _ecsWorld?.Destroy();
            _ecsWorld = null;
        }
    }
}
