using UnityEngine;
using NaughtyAttributes;
using SkyCrush.Utility;

namespace SkyCrush.WSGenerator
{

    [ExecuteInEditMode]
    public class AreaSettingsUpdater : MonoBehaviour
    {
#if UNITY_EDITOR
        private const string AreaContainerName = "Areas";

        [Button("Settings")]
        [SerializeField]
        private void GoToSettings() => Settings.Select();

        private AreaSettings AreaSettings => Settings.Instance.AreaSettings;
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

        public void UpdateAreas()
        {
            if (Application.isPlaying) return;

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

            var _areasInfo = AreaSettings.AreasInfo;
            _areas = new Area2D[_areasInfo.Length];

            for(var i = 0; i < _areas.Length; i++)
            {
                var newArea = Instantiate(AreaSettings.AreaPrefab, _areasContainer);

                newArea.name = "Area2D_" + i;
                newArea.Init(_areasInfo[i]);

                _areas[i] = newArea;
            }
        }
#endif
    }
}
