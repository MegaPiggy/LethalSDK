using LethalSDK.Component;
using LethalSDK.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;

namespace LethalSDK.Editor
{
    public class EditorChecker : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        }
    }
    [CustomEditor(typeof(SI_DungeonGenerator))]
    public class SI_DungeonGeneratorEditor : EditorChecker
    {
        public override void OnInspectorGUI()
        {
            SI_DungeonGenerator t = (SI_DungeonGenerator)target;

            String path = AssetDatabase.GetAssetPath(t.DungeonRoot);
            if (path != null && path.Length > 0)
            {
                EditorGUILayout.HelpBox("Dungeon Root must be in the scene.", MessageType.Error);
            }

            base.OnInspectorGUI();
        }
    }
    [CustomEditor(typeof(SI_ScanNode))]
    public class SI_ScanNodeEditor : EditorChecker
    {
        public override void OnInspectorGUI()
        {
            SI_ScanNode t = (SI_ScanNode)target;

            if(t.MinRange > t.MaxRange)
            {
                EditorGUILayout.HelpBox("Min Range must be smaller than Max Ranger.", MessageType.Error);
            }

            base.OnInspectorGUI();
        }
    }
    [CustomEditor(typeof(Scrap))]
    public class ScrapEditor : EditorChecker
    {
        public override void OnInspectorGUI()
        {
            Scrap t = (Scrap)target;
            
            if(t.prefab == null)
            {
                EditorGUILayout.HelpBox("You must add a Prefab to your Scrap.", MessageType.Info);
            }
            else
            {
                if(t.prefab.GetComponent<NetworkObject>() == null)
                {
                    EditorGUILayout.HelpBox("The Prefab must have a NetworkObject.", MessageType.Error);
                }
                if(t.prefab.transform.Find("ScanNode") == null)
                {
                    EditorGUILayout.HelpBox("The Prefab don't have a ScanNode.", MessageType.Warning);
                }
                if (AssetModificationProcessor.ExtractBundleNameFromPath(AssetDatabase.GetAssetPath(t.prefab)) != AssetModificationProcessor.ExtractBundleNameFromPath(AssetDatabase.GetAssetPath(t)))
                {
                    EditorGUILayout.HelpBox("The Prefab must come from the same mod as your Scrap.", MessageType.Warning);
                }
            }
            if (t.itemName == null || t.itemName.Length == 0)
            {
                EditorGUILayout.HelpBox("You scrap must have a Name.", MessageType.Error);
            }
            if (!t.useGlobalSpawnWeight && !t.perPlanetSpawnWeight().Any(w => w.SceneName != null && w.SceneName.Length > 0))
            {
                EditorGUILayout.HelpBox("Your scrap use Per Planet Spawn Weight but no planet are defined.", MessageType.Warning);
            }

            base.OnInspectorGUI();
        }
    }
}
