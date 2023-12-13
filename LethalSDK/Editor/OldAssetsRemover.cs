using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LethalSDK.Editor
{
    internal class OldAssetsRemover
    {
        private static readonly List<string> assetPaths = new List<string>
        {
            "Assets/Path/To/Your/Asset1.asset",
            "Assets/Path/To/Your/Asset2.prefab",
            "Assets/Path/To/Your/Folder"
        };

        [InitializeOnLoadMethod]
        public static void CheckOldAssets()
        {
            foreach (var path in assetPaths)
            {
                if (AssetDatabase.IsValidFolder(path))
                {
                    DeleteFolder(path);
                }
                else if (AssetDatabase.LoadAssetAtPath<GameObject>(path) != null)
                {
                    DeleteAsset(path);
                }
            }
        }
        private static void DeleteFolder(string path)
        {
            if (AssetDatabase.DeleteAsset(path))
            {
                Debug.Log("Deleted folder at: " + path);
            }
            else
            {
                Debug.LogError("Failed to delete folder at: " + path);
            }
        }

        private static void DeleteAsset(string path)
        {
            if (AssetDatabase.DeleteAsset(path))
            {
                Debug.Log("Deleted asset at: " + path);
            }
            else
            {
                Debug.LogError("Failed to delete asset at: " + path);
            }
        }
    }
}
