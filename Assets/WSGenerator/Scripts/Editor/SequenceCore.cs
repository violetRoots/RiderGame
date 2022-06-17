using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace SkyCrush.WSGenerator
{
    public partial class Sequence
    {
        //private const string TypeIndexDropdownName = nameof(typeIndexes);
        //private const string OnChangeTypeIndexMethodName = nameof(UpdateAdditionalDataType);

        public ref PoolInfo[] PoolsInfo => ref poolsInfo;

        //private int[] typeIndexes;
        //private List<Type> dataTypes;
        //private Type selectedType;

        private void OnValidate()
        {
            UpdateStageValues();
        }

        //todo в общем, здесь уже реаллизованы выбор и смена типов пользовательских калссов с данными по атрибуту,
        //todo осталось добавить прорисовку
        //private void UpdateAdditionalDataType()
        //{
        //    dataTypes = new List<Type>();

        //    AppDomain currentDomain = AppDomain.CurrentDomain;
        //    var assemblies = currentDomain.GetAssemblies();
        //    foreach(var assembly in assemblies)
        //    {
        //        foreach(var type in assembly.GetTypes()) 
        //        {
        //            if (type.GetCustomAttributes<AdditionalStageDataAttribute>().Count() > 0) dataTypes.Add(type);
        //        }
        //    }

        //    Debug.Log($"{nameof(dataTypes)} {dataTypes.Count}");
        //    Debug.Log($"{nameof(dataTypes)} {dataTypes[0].Name}");

        //    typeIndexes = new int[dataTypes.Count];
        //    for (var i = 0; i < dataTypes.Count; i++) typeIndexes[i] = i;

        //    Debug.Log($"{nameof(typeIndexes)} {typeIndexes.Length}");
        //    Debug.Log($"{nameof(typeIndex)} {typeIndex}");

        //    selectedType = dataTypes[typeIndex];
        //    additionalDataType = selectedType.Name;
        //    data = Activator.CreateInstance(selectedType);

        //    Debug.Log($"{nameof(data)} {data.GetType().Name}");
        //}

        private void UpdateStageValues()
        {
            var areaContainer = Settings.Instance.AreaContainer;

            foreach (var fixedStage in fixedStages)
            {
                fixedStage.UpdatePool(ref poolsInfo);
                fixedStage.UpdateAreas(areaContainer);
            }

            foreach (var randomStage in randomStages)
            {
                randomStage.UpdatePool(ref poolsInfo);
                randomStage.UpdateAreas(areaContainer);
            }
        }
    }
}
