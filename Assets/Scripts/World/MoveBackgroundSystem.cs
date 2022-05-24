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

        private EcsFilter<Input, Background> _filter;
        private Vector2 _textureOffset;

        public void Run()
        {
            foreach(var i in _filter)
            {
                ref var input = ref _filter.Get1(i);
                ref var background = ref _filter.Get2(i);

                _textureOffset.y += -_levelData.currentWorldSpeed;
                _textureOffset.x += input.horizontal * _levelData.currentLevelConfig.baseXSpeed;
                background.renderer.material.mainTextureOffset = _textureOffset * BackgroundSpeedMultiplier;
            }
        }
    }
}