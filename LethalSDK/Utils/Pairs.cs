using LethalSDK.ScriptableObjects;
using System;
using UnityEngine;

namespace LethalSDK.Utils
{
    [Serializable]
    public struct StringIntPair
    {
        public string _string;
        public int _int;
        public StringIntPair(string _string, int _int)
        {
            this._string = _string;
            this._int = Mathf.Clamp(_int, 0, 100);
        }
    }
    [Serializable]
    public struct StringStringPair
    {
        public string _string1;
        public string _string2;
        public StringStringPair(string _string1, string _string2)
        {
            this._string1 = _string1;
            this._string2 = _string2;
        }
    }
    [Serializable]
    public struct ScrapSpawnChance
    {
        public string SceneName;
        public int SpawnWeight;
        public ScrapSpawnChance(string sceneName, int spawnWeight)
        {
            this.SceneName = sceneName;
            this.SpawnWeight = Mathf.Clamp(spawnWeight, 0, 100);
        }
    }
    [Serializable]
    public struct ScrapInfoPair
    {
        public string ScrapPath;
        public Scrap Scrap;
        public ScrapInfoPair(string scrapPath, Scrap scrap)
        {
            this.ScrapPath = scrapPath;
            this.Scrap = scrap;
        }
    }
    [Serializable]
    public struct AudioClipInfoPair
    {
        public string AudioClipName;
        [HideInInspector]
        public string AudioClipPath;
        [SerializeField]
        public AudioClip AudioClip;
        public AudioClipInfoPair(string audioClipName, string audioClipPath)
        {
            this.AudioClipName = audioClipName;
            this.AudioClipPath = audioClipPath;
            AudioClip = null;
        }
    }
}
