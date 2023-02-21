using UnityEngine;
using RiderGame.Gameplay;

namespace RiderGame.SO
{
    public partial class NpcConfiguration
    {
        private void UpdateModifierContainers(string path)
        {
            foreach (var modifierContainer in modifiers)
            {
                modifierContainer.InitValue(path);
            }
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
