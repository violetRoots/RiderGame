using UnityEngine;

namespace SkyCrush.WSGenerator
{
    public class AreaManager
    {
        private AreaSettings _areaSettings;
        private Generator _generator;
        private Area2D[] _areas;

        public void Init(Settings settings, Generator generator)
        {
            _areaSettings = settings.AreaSettings;
            _generator = generator;
            _areas = _generator.GetComponentsInChildren<Area2D>();

            for(var i = 0; i < _areas.Length; i++)
            {
                _areas[i].Init(_areaSettings.AreasInfo[i]);
            }
        }

        public Area2D GetArea(int areaIndex)
        {
            return _areas[areaIndex];
        }
    }
}
