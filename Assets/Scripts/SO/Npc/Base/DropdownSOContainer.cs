using NaughtyAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using RiderGame.Gameplay;

namespace RiderGame.SO
{
    public abstract class DropdownSOContainer<T> where T : ScriptableObject
    {
        private string[] DropdownNames => GetDropdownDictionary().Keys.ToArray();

        public string DropdownName => currentDropdownName;
        public T Value => value;

        [OnValueChanged(nameof(UpdateValue))]
        [Dropdown(nameof(DropdownNames))]
        [SerializeField]
        private string currentDropdownName;

        [Expandable]
        [SerializeField]
        private T value;

        [HideInInspector]
        [SerializeField]
        private string previousValueName;

        [HideInInspector]
        [SerializeField]
        private string parentDirectory;

        public void InitValue<S>(string path)
        {
            var newDropdownName = DropdownNames.Where(s => s == typeof(S).Name).FirstOrDefault();
            if (string.IsNullOrEmpty(newDropdownName)) return;

            currentDropdownName = newDropdownName;

            InitValue(path);
        }

        public void InitValue(string path)
        {
            parentDirectory = path;

            UpdateValue();
        }

        private void UpdateValue()
        {
            if (currentDropdownName == previousValueName) return;

            if (currentDropdownName == string.Empty)
                currentDropdownName = DropdownNames[0];

            var modifierType = GetDropdownDictionary()[currentDropdownName];

            var modifierDiractoryPath = GetCurrentDirectoryPath(parentDirectory);
            var modifierPath = GetCurrentValuePath(parentDirectory, currentDropdownName);

            if (!AssetDatabase.IsValidFolder(modifierDiractoryPath))
                AssetDatabase.CreateFolder(parentDirectory, GetCurrentDirectoryName());

            value = (T) AssetDatabase.LoadAssetAtPath(modifierPath, modifierType);

            if (value == null)
            {
                value = (T) ScriptableObject.CreateInstance(modifierType);
                AssetDatabase.CreateAsset(value, modifierPath);
            }

            previousValueName = currentDropdownName;
        }

        protected abstract Dictionary<string, Type> GetDropdownDictionary();
        protected abstract string GetCurrentDirectoryName();
        protected abstract string GetCurrentDirectoryPath(string parentDictionary);
        protected abstract string GetCurrentValuePath(string parentDirectory, string currentDropdown);
    }
}
