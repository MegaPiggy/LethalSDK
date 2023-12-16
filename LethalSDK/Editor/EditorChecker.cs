using LethalSDK.Component;
using LethalSDK.ScriptableObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                EditorGUILayout.HelpBox("Min Range must be smaller than Max Ranger", MessageType.Error);
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
                EditorGUILayout.HelpBox("You must add a prefab to your Scrap", MessageType.Info);
            }

            base.OnInspectorGUI();
        }
    }
}
