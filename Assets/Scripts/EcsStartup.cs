using UnityEngine;
using Leopotam.Ecs;
using Voody.UniLeo;
using RiderGame.Inputs;
using RiderGame.Player;

public class EcsStartup : MonoBehaviour
{
    private EcsWorld _ecsWorld;
    private EcsSystems _systems;

    private void Start()
    {
        _ecsWorld = new EcsWorld();
        _systems = new EcsSystems(_ecsWorld);

        _systems
            .ConvertScene()
            .Add(new InputSystem())
            .Add(new PlayerMovementSystem())
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
