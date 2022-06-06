using UnityEngine;

namespace RiderGame.SO
{
    public class SingletonScriptableObject<T> : ScriptableObject where T : SingletonScriptableObject<T>
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if(instance == null)
                {
                    var assets = Resources.LoadAll<T>("Singletons");
                    if(assets == null || assets.Length < 1)
                    {
                        Debug.LogError($"The instance of {typeof(T).FullName} cannot be load. Check existing of this file.");
                    }
                    if (assets.Length > 1)
                    {
                        Debug.LogError($"The are more then one instance of {nameof(T)} singleton.");
                    }
                    instance = assets[0];
                }
                return instance;
            }
        }
    }
}
