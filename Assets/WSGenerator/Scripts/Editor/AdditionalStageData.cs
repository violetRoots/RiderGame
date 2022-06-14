using System;
using UnityEngine;

namespace SkyCrush.WSGenerator
{
    //todo ����� ������ ������ ���������� ���� �� ������, �� �������, ����� �������� ���������� ����������� ��� ���������� ����� ��������� (������ SequenceCore)
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
