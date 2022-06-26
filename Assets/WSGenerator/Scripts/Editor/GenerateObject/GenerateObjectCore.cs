using System;
using UnityEngine;
using NaughtyAttributes;

namespace SkyCrush.WSGenerator
{
    public partial class GenerateObject
    {
        public const float CurveRange = 10.0f;

        public GameObject Instance => instance;
        public AreaInfo Area => areaValue;
        public AnimationCurve FrequencyCurve => frequencyCurve;

        private const string InstanceDropdownName = nameof(_instances);
        private const string AreaDropdownName = nameof(_areaIndexes);

        private GameObject[] _instances = new GameObject[1] { null };
        private PoolInfo[] _poolsInfo;

        private int[] _areaIndexes;
        private AreaInfo[] _areaValues;

        public void UpdateAreaValue()
        {
            _areaValues = Settings.Instance.AreaContainer.areas;

            _areaIndexes = new int[_areaValues.Length];
            for (var i = 0; i < _areaValues.Length; i++) _areaIndexes[i] = i;

            areaIndex = Mathf.Clamp(areaIndex, 0, _areaIndexes.Length - 1);
            areaValue = _areaValues[areaIndex];
        }


        public void UpdatePool(ref PoolInfo[] poolsInfo)
        {
            _poolsInfo = poolsInfo;
            _instances = new GameObject[_poolsInfo.Length];

            for (var i = 0; i < _poolsInfo.Length; i++)
            {
                _instances[i] = _poolsInfo[i].instance;
            }
        }

        public void UpdateCurveDescription(float duration)
        {
            x = $"time: 1 unit = duration / {CurveRange} ({(float) (duration / CurveRange)} sec)";
            y = $"frequency: 1 unit = 1 obj / {Settings.Instance.FrequencySecondsPerUnit} sec ({(float)(1 / Settings.Instance.FrequencySecondsPerUnit)} obj/sec)";

            var count = 0;
            var time = 0.0f;
            while(time < duration)
            {
                var process = Mathf.Clamp01((float)(time / duration));
                var frequency = (float) (frequencyCurve.Evaluate(process * CurveRange) / Settings.Instance.FrequencySecondsPerUnit);
                var clampFrequency = Mathf.Clamp(frequency, Settings.Instance.MinFrequencyGenerationValue, Settings.Instance.MaxFrequencyGenerationValue);

                count++;
                time += (float)(1 / clampFrequency);
            }

            var countSize = count.ToString().Length;
            var correction = countSize < 3 ? 1 : Mathf.Pow(10, (countSize-2));
            objectsCount = $"{count}+-{correction}";
        }
    }
}
