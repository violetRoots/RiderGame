using System;
using UnityEngine;
using NaughtyAttributes;

namespace SkyCrush.WSGenerator
{

    [Serializable]
    public partial class GenerateObject
    {
        [Header("Instance")]

        [Dropdown(InstanceDropdownName)]
        [AllowNesting]
        [SerializeField]
        private GameObject instance;

        [Header("Generate Area")]

        [Dropdown(AreaDropdownName)]
        [AllowNesting]
        [SerializeField]
        private int areaIndex;
        [ReadOnly]
        [AllowNesting]
        [SerializeField]
        private AreaInfo areaValue;


        [Header("Generate Process")]
        [AllowNesting]
        [SerializeField]
        [CurveRange(0, 0, CurveRange, CurveRange, EColor.Red)]
        private AnimationCurve frequencyCurve;

        [AllowNesting]
        [ReadOnly]
        [SerializeField]
        private string x, y;

        [AllowNesting]
        [ReadOnly]
        [SerializeField]
        private string objectsCount;
    }
}

