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
            return ModifiersInfo.ModifierDirectoryName;
        }

        protected override string GetCurrentDirectoryPath(string parentDictionary)
        {
            return ModifiersInfo.GetModifiersDirectoryPath(parentDictionary);
        }

        protected override string GetCurrentValuePath(string parentDirectory, string currentDropdown)
        {
            return ModifiersInfo.GetModifierPath(parentDirectory, currentDropdown);
        }

        protected override Dictionary<string, Type> GetDropdownDictionary()
        {
            return ModifiersInfo.All;
        }
    }
}
