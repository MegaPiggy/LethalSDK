using UnityEngine;
using UnityEditor;
using System;

namespace LethalSDK.Editor
{
    public class Lethal_AssetBundleBuilderWindow : EditorWindow
    {
        private static string assetBundleDirectoryKey = "LethalSDK_AssetBundleBuilderWindow_assetBundleDirectory";
        private static string uncompressedAssetBundleKey = "LethalSDK_AssetBundleBuilderWindow_uncompressedAssetBundle";
        private static string _64BitsAssetBundleKey = "LethalSDK_AssetBundleBuilderWindow_64BitsAssetBundleKey";

        string assetBundleDirectory = string.Empty;
        bool uncompressedAssetBundle;
        bool _64BitsAssetBundle;

        [MenuItem("LethalSDK/AssetBundle Builder")]
        public static void ShowWindow()
        {
            Lethal_AssetBundleBuilderWindow window = GetWindow<Lethal_AssetBundleBuilderWindow>("AssetBundle Builder");
            window.minSize = new Vector2(295, 133);
            window.maxSize = new Vector2(295, 133);
            window.LoadPreferences();
        }

        void OnGUI()
        {
            GUILayout.Label("Base Settings", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(new GUIContent("Output Path", "The directory where the asset bundles will be saved."), GUILayout.Width(84));
            assetBundleDirectory = EditorGUILayout.TextField(assetBundleDirectory, GUILayout.Width(200));
            GUILayout.EndHorizontal();

            EditorGUILayout.Space(5);

            GUILayout.Label("Options", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(new GUIContent("Uncompressed Asset Bundle", "Check this to build the asset bundle without compression."), GUILayout.Width(270));
            uncompressedAssetBundle = EditorGUILayout.Toggle(uncompressedAssetBundle);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(new GUIContent("64 Bits Asset Bundle (Not recommended)", "Better performances but incompatible with 32 bits computers."), GUILayout.Width(270));
            _64BitsAssetBundle = EditorGUILayout.Toggle(_64BitsAssetBundle);
            GUILayout.EndHorizontal();

            EditorGUILayout.Space(5);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Build AssetBundles", GUILayout.Width(240)))
            {
                BuildAssetBundles();
            }
            if (GUILayout.Button("Reset", GUILayout.Width(45)))
            {
                ClearPreferences();
            }
            GUILayout.EndHorizontal();
        }
        void ClearPreferences()
        {
            EditorPrefs.DeleteKey(assetBundleDirectoryKey);
            EditorPrefs.DeleteKey(uncompressedAssetBundleKey);
            EditorPrefs.DeleteKey(_64BitsAssetBundleKey);
            LoadPreferences();
        }

        void BuildAssetBundles()
        {
            if (!System.IO.Directory.Exists(assetBundleDirectory))
            {
                System.IO.Directory.CreateDirectory(assetBundleDirectory);
            }

            BuildAssetBundleOptions options = uncompressedAssetBundle ? BuildAssetBundleOptions.UncompressedAssetBundle : BuildAssetBundleOptions.None;
            BuildTarget target = _64BitsAssetBundle ? BuildTarget.StandaloneWindows64 : BuildTarget.StandaloneWindows;

            try
            {
                if(assetBundleDirectory != null || assetBundleDirectory.Length != 0)
                {
                    AssetBundleManifest manifest = BuildPipeline.BuildAssetBundles(assetBundleDirectory, options, target);
                    if(manifest != null)
                    {
                        Debug.Log("AssetBundles built successfully.");
                    }
                    else
                    {
                        Debug.LogError("Cannot build AssetBundles.");
                    }
                }
                else
                {
                    Debug.LogError("AssetBundles path cannot be blank.");
                }
            }
            catch(Exception ex)
            {
                Debug.LogError(ex.Message);
            }

        }
        void OnLostFocus()
        {
            SavePreferences();
        }

        void OnDisable()
        {
            SavePreferences();
        }

        void LoadPreferences()
        {
            assetBundleDirectory = EditorPrefs.GetString(assetBundleDirectoryKey, "Assets/AssetBundles");
            uncompressedAssetBundle = EditorPrefs.GetBool(uncompressedAssetBundleKey, false);
            _64BitsAssetBundle = EditorPrefs.GetBool(_64BitsAssetBundleKey, false);
        }

        void SavePreferences()
        {
            EditorPrefs.SetString(assetBundleDirectoryKey, assetBundleDirectory);
            EditorPrefs.SetBool(uncompressedAssetBundleKey, uncompressedAssetBundle);
            EditorPrefs.SetBool(_64BitsAssetBundleKey, _64BitsAssetBundle);
        }
    }
}