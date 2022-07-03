using UnityEngine;
using Leopotam.Ecs;
using Voody.UniLeo;
using LeoEcsPhysics;
using SkyCrush.WSGenerator;
using RiderGame.Inputs;
using RiderGame.SO;
using RiderGame.World;
using RiderGame.Level;
using RiderGame.Physics;
using RiderGame.Gameplay;

namespace RiderGame
{
    public class EcsStartup : MonoBehaviour
    {
        [SerializeField] private GameConfiguration _gameConfigs;
        [SerializeField] private Generator _generator;

        private RuntimeLevelData _runtimeLevelData;

        private EcsStartup _ecsStartupObject;
        private EcsWorld _ecsWorld;
        private EcsSystems _systems;

        private void Start()
        {
            _ecsStartupObject = this;

            _ecsWorld = new EcsWorld();
            _systems = new EcsSystems(_ecsWorld);

            EcsPhysicsEvents.ecsWorld = _ecsWorld;

            _runtimeLevelData = new RuntimeLevelData();

            _systems
                .ConvertScene()
                .Inject(_ecsStartupObject)
                .Inject(_gameConfigs)
                .Inject(_generator)
                .Inject(_runtimeLevelData)
                .Add(new OneFramePhysicsSystem())
                .Add(new PlayerCollisionSystem())
                .Add(new UpdateRuntimeLevelDataSystem())
                .Add(new InputSystem())
                .Add(new ObjectActivationSystem())
                .Add(new ObjectOverlaySystem())
                .Add(new MoveWorldObjectSystem())
                .Add(new MoveBackgroundSystem())
                .Init();
        }


        private void Update()
        {
            _systems?.Run();
        }

        private void OnDestroy()
        {
            EcsPhysicsEvents.ecsWorld = null;
            _systems?.Destroy();
            _systems = null;
            _ecsWorld?.Destroy();
            _ecsWorld = null;
        }
    }
}
