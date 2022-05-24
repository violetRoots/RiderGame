using UnityEngine;
using Leopotam.Ecs;
using Voody.UniLeo;
using RiderGame.Inputs;
using RiderGame.SO;
using RiderGame.World;
using RiderGame.Level;

public class EcsStartup : MonoBehaviour
{
    [SerializeField] private GameConfiguration gameConfigs;

    private RuntimeLevelData _runtimeLevelData;

    private EcsWorld _ecsWorld;
    private EcsSystems _systems;

    private void Start()
    {
        _ecsWorld = new EcsWorld();
        _systems = new EcsSystems(_ecsWorld);

        _runtimeLevelData = new RuntimeLevelData();

        _systems
            .ConvertScene()
            .Inject(gameConfigs)
            .Inject(_runtimeLevelData)
            .Add(new UpdateRuntimeLevelDataSystem())
            .Add(new InputSystem())
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
        _systems?.Destroy();
        _systems = null;
        _ecsWorld?.Destroy();
        _ecsWorld = null;
    }
}
