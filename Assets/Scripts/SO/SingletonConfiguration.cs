using UnityEngine;
using UnityEditor;
using SkyCrush.Utility;

namespace RiderGame.SO
{
    public class SingletonConfiguration<T> : SingletonSOEditorOnly<T> where T : SingletonSOEditorOnly<T>
    {
        private const string NewLoadPath = "Configs/Singletons/";

        public static new T Instance
        {
            get
            {
                LoadPath = NewLoadPath;
                return SingletonSOEditorOnly<T>.Instance;
            }
        }
    }
}
