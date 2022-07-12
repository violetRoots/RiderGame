using UnityEngine;
using Leopotam.Ecs;
using RiderGame.Level;
using RiderGame.SO;
using Input = RiderGame.Inputs.Input;

namespace RiderGame.World
{
    public class MoveBackgroundSystem : IEcsRunSystem
    {
        private const float BackgroundSpeedMultiplier = 0.0004f;

        private readonly RuntimeLevelData _levelData;

        private EcsFilter<Background> _filter;
        private Vector3 _textureOffset;

        public void Run()
        {
            foreach(var i in _filter)
            {
                ref var background = ref _filter.Get1(i);

                var offset = Quaternion.Euler(0, 0, _levelData.MovementDirection) * new Vector3(0, _levelData.MovementSpeed, 0);
                Debug.Log(offset);
                _textureOffset -= offset;
                background.renderer.material.mainTextureOffset = _textureOffset * BackgroundSpeedMultiplier;
            }
        }
    }
}