using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace LethalSDK.Editor
{
    internal class OldAssetsRemover
    {
        private static readonly List<string> assetPaths = new List<string>
        {
            "Assets/LethalCompanyAssets",
            "Assets/Mods/LethalExpansion/Audio",
            "Assets/Mods/LethalExpansion/AudioMixerController",
            "Assets/Mods/LethalExpansion/Materials/Default.mat",
            "Assets/Mods/LethalExpansion/Prefabs/Settings",
            "Assets/Mods/LethalExpansion/Prefabs/EntranceTeleport.prefab",
            "Assets/Mods/LethalExpansion/Prefabs/Prefabs.zip",
            "Assets/Mods/LethalExpansion/Scenes/ItemPlaceTest",
            "Assets/Mods/LethalExpansion/Sprites/HandIcon.png",
            "Assets/Mods/LethalExpansion/Sprites/HandIconPoint.png",
            "Assets/Mods/LethalExpansion/Sprites/HandLadderIcon.png",
            "Assets/Mods/TemplateMod/Moons/NavMesh-Environment.asset",
            "Assets/Mods/TemplateMod/Moons/OldSeaPort.asset",
            "Assets/Mods/TemplateMod/Moons/Sky and Fog Global Volume Profile.asset",
            "Assets/Mods/TemplateMod/Moons/Sky and Fog Global Volume Profile 1.asset",
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
