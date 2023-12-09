using UnityEngine;
using UnityEditor;

public class AssetModificationProcessor : AssetPostprocessor
{
    private static void OnPostprocessAllAssets(
        string[] importedAssets,
        string[] deletedAssets,
        string[] movedAssets,
        string[] movedFromAssetPaths)
    {
        foreach (string assetPath in importedAssets)
        {
            ProcessAsset(assetPath);
        }

        foreach (string assetPath in movedAssets)
        {
            ProcessAsset(assetPath);
        }
    }
    private static void ProcessAsset(string assetPath)
    {
        if (assetPath.Contains("NavMesh-Environment"))
        {
            AssetDatabase.RenameAsset(assetPath, SelectionLogger.name != string.Empty ? SelectionLogger.name : "New NavMesh");
        }
        if (assetPath.StartsWith("Assets/Mods/") && assetPath.Split('/').Length > 3 && !assetPath.EndsWith(".unity"))
        {
            var asset = AssetImporter.GetAtPath(assetPath);
            if (asset != null)
            {
                string bundleName = ExtractBundleNameFromPath(assetPath);

                asset.assetBundleName = bundleName;

                Debug.Log($"{assetPath} asset moved to {bundleName} asset bundle.");
            }
        }
        else
        {
            var asset = AssetImporter.GetAtPath(assetPath);
            if (asset != null)
            {
                asset.assetBundleName = null;

                Debug.Log($"{assetPath} asset removed from asset bundle.");
            }
        }
    }
    private static string ExtractBundleNameFromPath(string path)
    {
        var pathSegments = path.Split('/');
        if (pathSegments.Length > 3)
        {
            return pathSegments[2].ToLower();
        }
        return "";
    }
}
[InitializeOnLoad]
public class SelectionLogger
{
    static SelectionLogger()
    {
        Selection.selectionChanged += OnSelectionChanged;
    }
    public static string name;

    private static void OnSelectionChanged()
    {
        if (Selection.activeGameObject != null)
        {
            name = GetRootParent(Selection.activeGameObject).name;
        }
        else
        {
            name = string.Empty;
        }
    }
    public static GameObject GetRootParent(GameObject obj)
    {
        if (obj != null && obj.transform.parent != null)
        {
            return GetRootParent(obj.transform.parent.gameObject);
        }
        else
        {
            return obj;
        }
    }
}
