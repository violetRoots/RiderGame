using System;
using UnityEngine;
using NaughtyAttributes;

namespace RiderGame.SO
{
    [Serializable]
    public class LevelStage
    {
        [Space(10)]
        public float duration = 10.0f;

        [Header("World")]
        public float maxYSpeed = 5.0f;
        public AnimationCurve ySpeedCurve;

        [Space(10)]
        [NonReorderable]
        public GenerateWorldObjectInfo[] objectInfo;

        public void UpdateGenerateAreas(ref GenerateAreaInfo[] generateAreas)
        {
            foreach (var objectInfo in objectInfo)
            {
                objectInfo.areaValues = generateAreas;

                objectInfo.UpdateGenerateAreaValue();
            }
        }

        public void UpdatePoolObjects(ref GameObject[] poolObjects)
        {
            foreach(var objectInfo in objectInfo) objectInfo.poolObjects = poolObjects;
        }
    }

    [Serializable]
    public class GenerateWorldObjectInfo
    {
        [Header("Instance")]

        [Dropdown("poolObjects")]
        [AllowNesting]
        public GameObject generateObject;

        [Header("Generate Area")]

        [OnValueChanged("UpdateGenerateAreaValue")]
        [Dropdown("areaIndexes")]
        [AllowNesting]
        public int areaIndex;
        [ReadOnly]
        [AllowNesting]
        public GenerateAreaInfo areaValue;


        [Header("Generate Process")]

        public int count = 1;
        //public AnimationCurve countCurve;

        [NonSerialized]
        public GameObject[] poolObjects;
        [NonSerialized]
        public int[] areaIndexes;
        [NonSerialized]
        public GenerateAreaInfo[] areaValues;

        public void UpdateGenerateAreaValue()
        {
            areaIndexes = new int[areaValues.Length];
            for (var i = 0; i < areaValues.Length; i++) areaIndexes[i] = i;

            areaIndex = Mathf.Clamp(areaIndex, 0, areaIndexes.Length - 1);
            areaValue = areaValues[areaIndex];
        }
    }
}
