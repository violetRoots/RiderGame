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

            CreateEmptyStateIfNeeded(path);
            CreateEmptyModifierIfNeeded(path);
        }
    }
}
