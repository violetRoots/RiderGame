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
using RiderGame.UI;

namespace RiderGame
{
    public class EcsStartup : MonoBehaviour
    {
        [SerializeField] private SessionStartup sessionStartup;
        [SerializeField] private UIConfiguration uiConfigs;
        [SerializeField] private GameConfiguration gameConfigs;
        [SerializeField] private Generator generator;

        private EcsStartup _ecsStartupObject;
        private EcsWorld _ecsWorld;
        private EcsSystems _UISystems;
        private EcsSystems _gameplaySystems;

        private void Awake()
        {
            _ecsStartupObject = this;

            _ecsWorld = new EcsWorld();
            _UISystems = new EcsSystems(_ecsWorld);
            _gameplaySystems = new EcsSystems(_ecsWorld);

            EcsPhysicsEvents.ecsWorld = _ecsWorld;

            _UISystems
                .ConvertScene()
                .Inject(uiConfigs)
                .Inject(sessionStartup.GameplayRuntimeData)
                .Inject(sessionStartup.SessionRuntimeData)

                //UI
                .Add(new UpperPanelSystem())
                .Add(new WayPointPanelSystem())
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
                .Add(new NpcCollisionSystem())

                //Runtime data updating
                .Add(new UpdateSessionDataSystem())
                .Add(new UpdateGameplayDataSystem())

                //Input
                .Add(new InputSystem())

                //World
                .Add(new GenerationSystem())
                .Add(new ObjectActivationSystem())
                .Add(new ObjectOverlaySystem())
                .Add(new MoveWorldObjectSystem())
                .Add(new MoveBackgroundSystem())

                //Player
                .Add(new PlayerAnimationSystem())
                .Add(new PlayerDashSystem())
                .Add(new BaseEffectSystem())
                .Add(new CoinCollectionSystem())
                .Add(new BringQuestSystem())

                //Npc
                .Add(new BaseNpcStateSystem())
                .Add(new NpcWalkStateSystem())
                .Add(new NpcAggressionStateSystem())

                //Animation
                .Add(new BaseAnimatorControllerSystem())
                .Add(new PlayerMovementAnimationSystem())

                //Event
                .Add(new OneFrameEventSystem())

                .Init();
        }

        private void Update()
        {
            _UISystems?.Run();

            if(sessionStartup.SessionRuntimeData != null &&
                sessionStartup.SessionRuntimeData.Status.Value == SessionStatus.Playing) 
                _gameplaySystems?.Run();
        }

        private void OnDestroy()
        {
            EcsPhysicsEvents.ecsWorld = null;
            _gameplaySystems?.Destroy();
            _gameplaySystems = null;
            _UISystems?.Destroy();
            _UISystems = null;
            _ecsWorld?.Destroy();
            _ecsWorld = null;
        }
    }
}
