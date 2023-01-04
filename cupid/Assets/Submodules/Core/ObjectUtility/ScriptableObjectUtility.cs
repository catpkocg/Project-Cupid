#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 

namespace Wayway.Engine
{
    /// <summary>
    /// ScriptableObject Utility Class. Only for in Editor Condition
    /// </summary>
    public static class ScriptableObjectUtility
    {
        public static T GetScriptableObject<T>() where T : ScriptableObject => GetScriptableObject<T>("");
        public static T GetScriptableObject<T>(string folderPath) where T : ScriptableObject => GetScriptableObject<T>(folderPath, "");

        /// <summary>
        /// Get Single ScriptableObject in folderPath with filter
        /// </summary>
        /// <typeparam name="T">ScriptableObject</typeparam>
        /// <param name="folderPath">Searching Target Path, ex Assets/Project</param>
        /// <param name="filter">Unity Filter rule, ex l:label, t:type</param>
        /// <returns>(if multiple), Return List.First()</returns>
        public static T GetScriptableObject<T>(string folderPath, string filter) where T : ScriptableObject
        {
            if (string.IsNullOrEmpty(folderPath)) folderPath = "Assets";
            if (string.IsNullOrEmpty(filter)) filter = $"t:{typeof(T).FullName}";

            var gUIDs = AssetDatabase.FindAssets(filter, new [] { folderPath });

            if (gUIDs == null || gUIDs.Length == 0)
            {
                Debug.Log($"Can't Find in {folderPath} of {typeof(T).FullName}");
                return null;
            }
            var targetIndex = 0;
            
            if (gUIDs.Length > 1)
            {
                for (var i = 0; i < gUIDs.Length; ++i)
                {
                    var assetName = AssetDatabase.GUIDToAssetPath(gUIDs[i]).Replace(folderPath, "")
                        .Replace(".asset", "");

                    if (assetName != filter) continue;
                    targetIndex = i;
                        
                    Debug.LogWarning($"Multiple Found :: count : {gUIDs.Length} keyWord : {filter} \n " +
                                     $"Select Asset by Exactly SameName :: {assetName}");
                }
            }

            var assetPath = AssetDatabase.GUIDToAssetPath(gUIDs[targetIndex]);
            
            var result = AssetDatabase.LoadAssetAtPath(assetPath, typeof(T)) as T;
            return result;
        }

        public static T[] GetScriptableObjects<T>() where T : ScriptableObject => GetScriptableObjects<T>("");
        public static T[] GetScriptableObjects<T>(string folderPath) where T : ScriptableObject => GetScriptableObjects<T>(folderPath, "");
        public static T[] GetScriptableObjects<T>(string folderPath, string filter) where T : ScriptableObject => GetScriptableObjectList<T>(folderPath, filter).ToArray();
        public static List<T> GetScriptableObjectList<T>() where T : ScriptableObject => GetScriptableObjectList<T>("");
        public static List<T> GetScriptableObjectList<T>(string folderPath) where T : ScriptableObject => GetScriptableObjectList<T>(folderPath, "");

        /// <summary>
        /// Get ScriptableObjects in folderPath with filter
        /// </summary>
        /// <typeparam name="T">ScriptableObject</typeparam>
        /// <param name="folderPath">Searching Target Path, ex Assets/Project</param>
        /// <param name="filter">Unity Filter rule, ex l:data, t:className</param>
        /// <returns>ScriptableObject List</returns>
        public static List<T> GetScriptableObjectList<T>(string folderPath, string filter) where T : ScriptableObject
        {
            var result = new List<T>();

            if (string.IsNullOrEmpty(folderPath)) folderPath = "Assets";
            if (string.IsNullOrEmpty(filter)) filter = $"t:{typeof(T).FullName}";

            var gUIDs = AssetDatabase.FindAssets(filter, new [] { folderPath });

            gUIDs.ForEach(x =>
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(x);
                var data = AssetDatabase.LoadAssetAtPath(assetPath, typeof(T)) as T;

                if (!result.Contains(data))
                    result.Add(data);
            });

            return result;
        }

        /// <summary>
        /// Create ScriptableObject at folderPath with className
        /// </summary>
        /// <param name="folderPath">target Path</param>
        /// <typeparam name="T">typeof specific ScriptableObject inherited class</typeparam>
        /// <returns>Created Object</returns>
        public static T CreateScriptableObject<T>(string folderPath) where T : ScriptableObject => CreateScriptableObject<T>(folderPath, "");
        
        /// <summary>
        /// Create ScriptableObject at folderPath with defaultName
        /// </summary>
        /// <param name="folderPath">target Path</param>
        /// <param name="defaultName">set Name. if Exist, auto suffix added</param>
        /// <typeparam name="T">typeof specific ScriptableObject inherited class</typeparam>
        /// <returns>Created Object</returns>
        public static T CreateScriptableObject<T>(string folderPath, string defaultName) where T : ScriptableObject
        {
            if (folderPath == null)
            {
                Debug.LogError("at least one of a Parameter value is <b><color=red>NULL!</color></b>");
                return null;
            }
            if (string.IsNullOrEmpty(defaultName))
            {
                defaultName = typeof(T).FullName;
                Debug.Log($"name Created by automatically <b><color=green>{typeof(T).FullName}</color></b>");
            }

            var data = ScriptableObject.CreateInstance<T>();
            var uniqueName = ObjectUtility.GetUniqueNameWithPath(folderPath, defaultName);

            AssetDatabase.CreateAsset(data, uniqueName);

            Debug.Log($"Create <b><color=green>{uniqueName}</color></b> Scriptable Object");

            return data;
        }

        /// <summary>
        /// Try Get typeof(T) class ScriptableObject
        /// </summary>
        /// <param name="result">if found, return result. else return null</param>
        /// <typeparam name="T">typeof ScriptableObject</typeparam>
        /// <returns>if found, return true, else return false.
        /// like Dictionary.TryGetValue()</returns>
        public static bool TryGetScriptableObject<T>(out T result) where T : ScriptableObject
        {
            result = GetScriptableObject<T>();
            
            return TryGetScriptableObject("", "", out result);
        }
        
        /// <summary>
        /// Try Get typeof(T) class ScriptableObject
        /// </summary>
        /// <param name="folderPath">searching Path</param>
        /// <param name="result">if found, return result. else return null</param>
        /// <typeparam name="T">typeof ScriptableObject</typeparam>
        /// <returns>if found, return true, else return false.
        /// like Dictionary.TryGetValue()</returns>
        public static bool TryGetScriptableObject<T>(string folderPath, out T result) where T : ScriptableObject
        {
            result = GetScriptableObject<T>(folderPath);
            
            return TryGetScriptableObject(folderPath, "", out result);
        }

        /// <summary>
        /// Try Get typeof(T) class ScriptableObject
        /// </summary>
        /// <param name="folderPath">searching Path</param>
        /// <param name="filter">unity string filter ex.t:ScriptableObject</param>
        /// <param name="result">if found, return result. else return null</param>
        /// <typeparam name="T">typeof ScriptableObject</typeparam>
        /// <returns>if found, return true, else return false.
        /// like Dictionary.TryGetValue()</returns>
        public static bool TryGetScriptableObject<T>(string folderPath, string filter, out T result) where T : ScriptableObject
        {
            result = GetScriptableObject<T>(folderPath, filter);

            return result;
        }
    }
}
#endif