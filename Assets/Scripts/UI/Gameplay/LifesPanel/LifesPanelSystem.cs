using Leopotam.Ecs;
using UnityEngine;
using UniRx;
using RiderGame.SO;
using RiderGame.RuntimeData;
using System.Collections.Generic;

namespace RiderGame.UI
{
    public class LifesPanelSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly GameConfiguration _gameConfigs;

        private readonly SessionRuntimeData _sessionRuntimeData;

        private readonly EcsFilter<UIElement, LifesPanel> _fLifesPanel;

        public void Init()
        {
            var maxLifesCount = _gameConfigs.GeneralCharacterConfiguration.MaxLifesCount;

            foreach (var i in _fLifesPanel)
            {
                var uiElement = _fLifesPanel.Get1(i);
                var lifesPanel = _fLifesPanel.Get2(i);

                lifesPanel.icons = new LifeIconController[maxLifesCount];
                for (var j = 0; j < maxLifesCount; j++)
                {
                    var lifeIcon = Object.Instantiate(lifesPanel.iconPrefab, uiElement.content.transform);

                    lifesPanel.icons[j] = lifeIcon;
                }

                lifesPanel.updateSubscription = _sessionRuntimeData.LifesCount.Subscribe((count) => OnLifesCountValueChanged(lifesPanel, count));
            }
        }

        public void Destroy()
        {
            foreach (var i in _fLifesPanel)
            {
                var lifesPanel = _fLifesPanel.Get2(i);

                lifesPanel.updateSubscription?.Dispose();
                lifesPanel.updateSubscription = null;
            }
        }

        private void OnLifesCountValueChanged(LifesPanel lifesPanel, int newValue)
        {
            for(var i = 0; i < lifesPanel.icons.Length; i++)
            {
                lifesPanel.icons[i].IsActiveIcon.Value = i + 1 <= newValue;
            }
        }
    }
}
