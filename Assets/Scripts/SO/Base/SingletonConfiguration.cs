using UnityEngine;
using UnityEditor;
using SkyCrush.Utility;

namespace RiderGame.SO
{
    public class SingletonConfiguration<T> : SingletonSOEditorOnly<T> where T : SingletonSOEditorOnly<T>
    {
#if UNITY_EDITOR
        private const string NewLoadPath = "Configs/Settings/";

        public static new T Instance
        {
            get
            {
                LoadPath = NewLoadPath;
                return SingletonSOEditorOnly<T>.Instance;
            }
        }
#endif
    }
}
