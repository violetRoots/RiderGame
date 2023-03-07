using RiderGame.SO;

namespace RiderGame.Gameplay
{
    public abstract class StatesInfo : DerivedClassesCollector<State>
    {
        public const string StateDirectoryName = "States [GENERATED]";

        public static string GetStatesDirectoryPath(string parentDirectoryPath)
        {
            return $"{parentDirectoryPath}/{StateDirectoryName}";
        }

        public static string GetStatePath(string parentDirectoryPath, string modifierName)
        {
            return $"{parentDirectoryPath}/{StateDirectoryName}/{modifierName}.asset";
        }
    }
}
