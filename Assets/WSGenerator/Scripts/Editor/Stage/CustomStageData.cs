using System;
using UnityEngine;
using NaughtyAttributes;

namespace SkyCrush.WSGenerator
{
    //todo этого класса внутри генератора быть не должно, он внешний, нужно доделать реализацию отображени€ доп информации через аттрибуты (смотри SequenceCore)
    [Serializable]
    public class CustomStageData
    {
        [Header("World")]
        [AllowNesting]
        [CurveRange(0, 0, 100, 1000, EColor.Blue)]
        [SerializeField]
        private AnimationCurve ySpeedCurve;
    }
}
