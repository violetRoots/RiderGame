using System;
using System.Collections.Generic;
using RiderGame.SO;

namespace RiderGame.Gameplay
{
    [Serializable]
    public class ModifierContainer : DropdownSOContainer<Modifier>
    {
        protected override string GetCurrentDirectoryName()
        {
            return Modifiers.ModifierDirectoryName;
        }

        protected override string GetCurrentDirectoryPath(string parentDictionary)
        {
            return Modifiers.GetModifiersDirectoryPath(parentDictionary);
        }

        protected override string GetCurrentValuePath(string parentDirectory, string currentDropdown)
        {
            return Modifiers.GetModifierPath(parentDirectory, currentDropdown);
        }

        protected override Dictionary<string, Type> GetDropdownDictionary()
        {
            return Modifiers.All;
        }
    }
}
