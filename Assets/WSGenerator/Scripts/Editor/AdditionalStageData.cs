using System;
using UnityEngine;

namespace SkyCrush.WSGenerator
{
    //todo этого класса внутри генератора быть не должно, он внешний, нужно доделать реализацию отображени€ доп информации через аттрибуты (смотри SequenceCore)
    [Serializable]
    [AdditionalStageData]
    public class AdditionalStageData
    {
        [Header("World")]
        [SerializeField]
        private float maxYSpeed = 5.0f;
        [SerializeField]
        private AnimationCurve ySpeedCurve;
    }
}
