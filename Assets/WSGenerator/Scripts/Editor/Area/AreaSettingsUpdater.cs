using UnityEngine;
using NaughtyAttributes;
using SkyCrush.Utility;

namespace SkyCrush.WSGenerator
{
#if UNITY_EDITOR
    [ExecuteInEditMode]
    public class AreaSettingsUpdater : MonoBehaviour
    {
        private const string AreaContainerName = "Areas";

        [Button("Settings")]
        [SerializeField]
        private void GoToSettings() => Settings.Select();

        private AreaSettings AreaSettings => Settings.Instance.AreaSettings;
        private Area2D AreaPrefab=> Settings.Instance.AreaSettings.AreaPrefab;
        private Transform _areasContainer;
        private Area2D[] _areas;


        private void OnEnable()
        {
            UpdateAreas();

            AreaSettings.SetUpdater(this);
        }

        private void OnDisable()
        {
            AreaSettings.SetUpdater(null);
        }

        private void OnDrawGizmos()
        {
            foreach (var area in _areas)
            {
                if (area == null || !area.Info.drawGizmos) continue;

                CustomGizmos.DrawRect(area.transform.position, area.BoxCollider.size, area.Info.color);
            }
        }

        public void UpdateAreas()
        {
            if(_areasContainer == null)
            {
                for (var i = 0; i < transform.childCount; i++)
                {
                    if (transform.GetChild(i).name != AreaContainerName) continue;

                    _areasContainer = transform.GetChild(i);
                }
            }
            
            if(_areasContainer != null)
            {
                DestroyImmediate(_areasContainer.gameObject);
            }

            _areasContainer = new GameObject(AreaContainerName).transform;
            _areasContainer.SetParent(transform);

            var _areasInfo = AreaSettings.Areas;
            _areas = new Area2D[_areasInfo.Length];

            for(var i = 0; i < _areas.Length; i++)
            {
                var newArea = Instantiate(AreaPrefab, _areasContainer);

                newArea.name = "Area2D_" + i;
                newArea.Init(_areasInfo[i]);

                _areas[i] = newArea;
            }
        }
    }
#endif
}
