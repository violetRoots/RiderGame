using RiderGame.SO;
using System;
using System.Collections.Generic;

namespace RiderGame.Gameplay
{
    [Serializable]
    public class StateContainer : DropdownSOContainer<State>
    {
        protected override string GetCurrentDirectoryName()
        {
            return StatesInfo.StateDirectoryName;
        }

        protected override string GetCurrentDirectoryPath(string parentDictionary)
        {
            return StatesInfo.GetStatesDirectoryPath(parentDictionary);
        }

        protected override string GetCurrentValuePath(string parentDirectory, string currentDropdown)
        {
            return StatesInfo.GetStatePath(parentDirectory, currentDropdown);
        }

        protected override Dictionary<string, Type> GetDropdownDictionary()
        {
            return StatesInfo.All;
        }
    }
}
