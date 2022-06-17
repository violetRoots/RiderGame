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

        [Range(1, 100)]
        [SerializeField]
        private int maxObjectsCount = 1;
        public AnimationCurve countCurve;
    }
}

