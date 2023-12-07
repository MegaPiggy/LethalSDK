using LethalSDK.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LethalSDK.ScriptableObjects
{
    [CreateAssetMenu(fileName = "ModManifest", menuName = "LethalSDK/Mod Manifest")]
    public class ModManifest : ScriptableObject
    {
        public string modName = "New Mod";
        [Space]
        public SerializableVersion version = new SerializableVersion(0, 0, 0, 0);
        [Space]
        public string author = "Author";
        [Space]
        [TextAreaAttribute]
        public string description = "Mod Description";
        [Space]
        [HeaderAttribute("Content")]
        public Scrap[] scraps = new Scrap[0];
        public Moon[] moons = new Moon[0];
        /*[HideInInspector]
        public string serializedData;
        private void OnValidate()
        {
            serializedData = string.Join(";", _scraps.Select(p => $"{p.ScrapName},{p.ScrapPath}"));
        }
        public ScrapInfoPair[] Scraps()
        {
            return serializedData.Split(';').Select(s => s.Split(',')).Where(split => split.Length == 2).Select(split => new ScrapInfoPair(split[0], split[1])).ToArray();
        }*/
        [Space]
        public AssetBank assetBank;
    }
}
