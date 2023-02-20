using RiderGame.Gameplay;
using System.Linq;
using UnityEditor;

namespace RiderGame.SO
{
    public partial class NpcConfiguration
    {
        private string[] StateNames => States.All.Keys.Where(stateName => states.Any(stateContainer => stateContainer.DropdownName == stateName)).ToArray();

        private void OnValidate()
        {
            var path = AssetDatabase.GetAssetPath(this);
            path = path.Substring(0, path.LastIndexOf('/'));

            UpdateModifierContainers(path);
            UpdateStateContainers(path);

            CreateEmptyStateIfNeeded(path);
            CreateEmptyModifierIfNeeded(path);
        }

        private void UpdateModifierContainers(string path)
        {
            foreach (var modifierContainer in modifiers)
            {
                modifierContainer.InitValue(path);
            }
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

        private void CreateEmptyModifierIfNeeded(string path)
        {
            if (modifiers.Count > 0) return;

            var emptyModifierContainer = new ModifierContainer();
            emptyModifierContainer.InitValue<EmptyModifier>(path);

            modifiers.Add(emptyModifierContainer);
        }
    }
}
