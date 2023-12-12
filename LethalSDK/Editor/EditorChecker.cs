using LethalSDK.Component;
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
            SI_DungeonGenerator generator = (SI_DungeonGenerator)target;

            String path = AssetDatabase.GetAssetPath(generator.DungeonRoot);
            if (path != null && path.Length > 0)
            {
                EditorGUILayout.HelpBox("Dungeon Root must be in the scene", MessageType.Error);
            }

            base.OnInspectorGUI();
        }
    }
}
