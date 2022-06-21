using System;
using UnityEngine;
using NaughtyAttributes;

namespace SkyCrush.WSGenerator
{
    //todo ����� ������ ������ ���������� ���� �� ������, �� �������, ����� �������� ���������� ����������� ��� ���������� ����� ��������� (������ SequenceCore)
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
