using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

namespace SkyCrush.WSGenerator
{
    public class SingletonSOEditorOnly<T> : ScriptableObject where T : SingletonSOEditorOnly<T>
    {
        public static T Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = LoadInstance();
                }
                return instance;
            }
        }

        public static string LoadPath { get; protected set; } = "WSGenerator/Settings/";

        private static T instance;

#if UNITY_EDITOR
        private static T LoadInstance()
        {
            var assets = LoadAssetsAtPath<T>(LoadPath);
            if (LoadPath == string.Empty)
            {
                Debug.LogError($"The instance of {typeof(T).FullName} cannot be load. Path to direactory is empty.");
                return null;
            }
            else if (assets == null || assets.Length == 0)
            {
                Debug.LogError($"The instance of {typeof(T).FullName} cannot be load. Check existing of this file.");
                return null;
            }
            else if (assets.Length > 1)
            {
                Debug.LogError($"The are more then one instance of {nameof(T)} singleton.");
                return assets[0];
            }
            else return assets[0];
        }

        public static T[] LoadAssetsAtPath<T>(string path)
        {

            ArrayList al = new ArrayList();
            string[] fileEntries = Directory.GetFiles(Application.dataPath + "/" + path);
            foreach (string fileName in fileEntries)
            {
                int index = fileName.LastIndexOf("/");
                string localPath = "Assets/" + path;

                if (index > 0)
                    localPath += fileName.Substring(index);

                Object t = AssetDatabase.LoadAssetAtPath(localPath, typeof(T));

                if (t != null)
                    al.Add(t);
            }
            T[] result = new T[al.Count];
            for (int i = 0; i < al.Count; i++)
                result[i] = (T)al[i];

            return result;
        }
    }
#endif
}
