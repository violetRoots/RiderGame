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
using RiderGame.Editor.CustomGizmos;

namespace RiderGame
{
    public class EcsStartup : MonoBehaviour
    {
        [SerializeField] private GameConfiguration _gameConfigs;
        [SerializeField] private Generator _generator;

        private RuntimeLevelData _runtimeLevelData;

        private EcsStartup _ecsStartupObject;
        private EcsWorld _ecsWorld;
        private EcsSystems _updateSystems;
        private EcsSystems _gizmosSystems;

        private void Start()
        {
            _ecsStartupObject = this;

            _ecsWorld = new EcsWorld();
            _updateSystems = new EcsSystems(_ecsWorld);

            EcsPhysicsEvents.ecsWorld = _ecsWorld;

            _runtimeLevelData = new RuntimeLevelData();

            _updateSystems
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
                .Add(new CharacterAnimationSystem())
                .Init();
#if UNITY_EDITOR
            _gizmosSystems = new EcsSystems(_ecsWorld);
            _gizmosSystems
                .Inject(_ecsStartupObject)
                .Inject(_gameConfigs)
                .Inject(_generator)
                .Inject(_runtimeLevelData)
                .Add(new DrawAnimationGizmosSystem())
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
