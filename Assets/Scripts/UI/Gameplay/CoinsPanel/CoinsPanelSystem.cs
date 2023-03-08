using UnityEngine;
using UniRx;
using Leopotam.Ecs;
using RiderGame.RuntimeData;

namespace RiderGame.UI
{
    public class CoinsPanelSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly SessionRuntimeData _sessionData;

        private readonly EcsFilter<UIElement, CoinsPanel> _fUpperPanel;

        public void Init()
        {
            _sessionData.CoinsCount.Subscribe(UpdateCoinsCount);
        }

        public void Destroy()
        {
            _sessionData.CoinsCount.Dispose();
        }

        private void UpdateCoinsCount(int value)
        {
            foreach(var i in _fUpperPanel)
            {
                ref var upperPanel = ref _fUpperPanel.Get2(i);

                upperPanel.coinsCountText.text = value.ToString();
            }
        }
    }
}
