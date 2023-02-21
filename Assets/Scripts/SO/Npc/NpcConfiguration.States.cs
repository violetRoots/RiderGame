using System.Linq;
using UnityEngine;
using RiderGame.Gameplay;

namespace RiderGame.SO
{
    public partial class NpcConfiguration
    {
        private string[] AttachedStatesNames => GetAttachedStatesNames();

        public bool TryGetState<T>(out T state) where T : State
        {
            var stateContainer = GetStateContainer<T>();

            if(stateContainer != null)
            {
                state = (T) stateContainer.Value;
                return true;
            }
            else
            {
                state = null;
                return false;
            }
        }

        public bool HasState<T>() where T : State
        {
            var stateContainer = GetStateContainer<T>();

            return stateContainer != null;
        }

        private StateContainer GetStateContainer<T>() where T : State
        {
            return states.Where(stateContainer => stateContainer.Value is T).FirstOrDefault();
        }

        private void UpdateStateContainers(string path)
        {
            foreach (var stateContainer in states)
            {
                stateContainer.InitValue(path);
            }
        }

        private void CreateEmptyStateIfNeeded(string path)
        {
            if (states.Count > 0) return;

            var emptyStateContainer = new StateContainer();
            emptyStateContainer.InitValue<EmptyState>(path);

            states.Add(emptyStateContainer);
        }

        private string[] GetAttachedStatesNames()
        {
            return States.All.Keys.Where(stateName => states.Any(stateContainer => stateContainer.DropdownName == stateName))
                                  .ToArray();
        }
    }
}
