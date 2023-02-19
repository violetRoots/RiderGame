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
            return States.StateDirectoryName;
        }

        protected override string GetCurrentDirectoryPath(string parentDictionary)
        {
            return States.GetStatesDirectoryPath(parentDictionary);
        }

        protected override string GetCurrentValuePath(string parentDirectory, string currentDropdown)
        {
            return States.GetStatePath(parentDirectory, currentDropdown);
        }

        protected override Dictionary<string, Type> GetDropdownDictionary()
        {
            return States.All;
        }
    }
}
