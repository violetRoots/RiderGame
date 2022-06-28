using System;
using UnityEngine;

namespace SkyCrush.WSGenerator
{
    public partial class AreaSettings
    {
        public Area2D AreaPrefab => areaPrefab;
        public AreaInfo[] Areas => areas;

        private AreaSettingsUpdater _settingsUpdater;

        private void OnValidate()
        {
            if (_settingsUpdater == null) return;

            UnityEditor.EditorApplication.delayCall += () =>
            {
                _settingsUpdater.UpdateAreas();
            };
        }

        public void SetUpdater(AreaSettingsUpdater updater)
        {
            _settingsUpdater = updater;
        }
    }
}
