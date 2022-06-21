using System;
using UnityEngine;

namespace SkyCrush.WSGenerator
{
    //todo ����� ������ ������ ���������� ���� �� ������, �� �������, ����� �������� ���������� ����������� ��� ���������� ����� ��������� (������ SequenceCore)
    [Serializable]
    public class CustomStageData
    {
        public float YSpeed => ySpeed;

        [SerializeField]
        private float ySpeed;
    }
}
