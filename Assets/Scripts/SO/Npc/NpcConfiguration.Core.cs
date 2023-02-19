using UnityEditor;

namespace RiderGame.SO
{
    public partial class NpcConfiguration
    {
        private void OnValidate()
        {
            var path = AssetDatabase.GetAssetPath(this);
            path = path.Substring(0, path.LastIndexOf('/'));

            UpdateModifierContainers(path);
            UpdateStateContainers(path);
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
    }
}
