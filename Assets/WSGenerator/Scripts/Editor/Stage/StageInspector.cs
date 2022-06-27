using System;
using UnityEngine;

namespace SkyCrush.WSGenerator
{
    [Serializable]
    public partial class Stage
    {
        [Space(10)]
        [SerializeField]
        private float duration;

        //todo ���������� - ��������� �� ������ ����� ������� ������ �� RiderGame
        [SerializeField]
        private CustomStageData customData;

        [Space(10)]
        [NonReorderable]
        [SerializeField]
        private GenerateObjectInfo[] generateObjects;
    }
}
