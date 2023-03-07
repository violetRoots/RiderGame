using RiderGame.SO;

namespace RiderGame.Gameplay
{
    public abstract class ModifiersInfo : DerivedClassesCollector<Modifier>
    {
        public const string ModifierDirectoryName = "Modifiers [GENERATED]";

        public static string GetModifiersDirectoryPath(string parentDirectoryPath)
        {
            return $"{parentDirectoryPath}/{ModifierDirectoryName}";
        }

        public static string GetModifierPath(string parentDirectoryPath, string modifierName)
        {
            return $"{parentDirectoryPath}/{ModifierDirectoryName}/{modifierName}.asset";
        }
    }
}
