using UnityEngine;
using NaughtyAttributes;

namespace SkyCrush.WSGenerator
{
    [CreateAssetMenu(fileName = "WSG_Sequence", menuName = "WSGenerator/Sequence", order = 1)]
    public partial class Sequence : ScriptableObject
    {
        [Button("Settings")]
        [SerializeField]
        private void GoToSettings() => Settings.Select();

        [ReorderableList]
        [SerializeField]
        private PoolInfo[] poolsInfo;

        //todo �������� ������ ������������ � ��������� ���������������� ������� � �������
            //[SerializeField]
            //[ReadOnly]
            //private string additionalDataType;
            //[SerializeField]
            //[OnValueChanged(OnChangeTypeIndexMethodName)]
            //[Dropdown(TypeIndexDropdownName)]
            //private int typeIndex;

            //[SerializeField]
            //private object data;

        [ReorderableList]
        [SerializeField]
        private Stage[] fixedStages;
        [ReorderableList]
        [SerializeField]
        private Stage[] randomStages;
    }
}