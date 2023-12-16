using LethalSDK.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace LethalSDK.ScriptableObjects
{
    [CreateAssetMenu(fileName = "AssetBank", menuName = "LethalSDK/Asset Bank")]
    public class AssetBank : ScriptableObject
    {
        [HeaderAttribute("Audio Clips")]
        [SerializeField]
        private AudioClipInfoPair[] _audioClips = new AudioClipInfoPair[0];
        [SerializeField]
        private PlanetPrefabInfoPair[] _planetPrefabs = new PlanetPrefabInfoPair[0];
        [HideInInspector]
        public string serializedAudioClips;
        public string serializedPlanetPrefabs;
        private void OnValidate()
        {
            for (int i = 0; i < _audioClips.Length; i++)
            {
                _audioClips[i].AudioClipName = _audioClips[i].AudioClipName.RemoveNonAlphanumeric(1);
                _audioClips[i].AudioClipPath = _audioClips[i].AudioClipPath.RemoveNonAlphanumeric(4);
            }
            for (int i = 0; i < _planetPrefabs.Length; i++)
            {
                _planetPrefabs[i].PlanetPrefabName = _planetPrefabs[i].PlanetPrefabName.RemoveNonAlphanumeric(1);
                _planetPrefabs[i].PlanetPrefabPath = _planetPrefabs[i].PlanetPrefabPath.RemoveNonAlphanumeric(4);
            }
            serializedAudioClips = string.Join(";", _audioClips.Select(p => $"{(p.AudioClipName.Length == 0 ? (p.AudioClip != null ? p.AudioClip.name : "") : p.AudioClipName)},{AssetDatabase.GetAssetPath(p.AudioClip)}"));
            serializedPlanetPrefabs = string.Join(";", _planetPrefabs.Select(p => $"{(p.PlanetPrefabName.Length == 0 ? (p.PlanetPrefab != null ? p.PlanetPrefab.name : "") : p.PlanetPrefabName)},{AssetDatabase.GetAssetPath(p.PlanetPrefab)}"));
        }
        public AudioClipInfoPair[] AudioClips()
        {
            return serializedAudioClips.Split(';').Select(s => s.Split(',')).Where(split => split.Length == 2).Select(split => new AudioClipInfoPair(split[0], split[1])).ToArray();
        }
        public bool HaveAudioClip(string audioClipName)
        {
            return AudioClips().Any(a => a.AudioClipName == audioClipName);
        }
        public string AudioClipPath(string audioClipName)
        {
            return AudioClips().First(c => c.AudioClipName == audioClipName).AudioClipPath;
        }
        public Dictionary<string, string> AudioClipsDictionary()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (var pair in _audioClips)
            {
                dictionary.Add(pair.AudioClipName, pair.AudioClipPath);
            }
            return dictionary;
        }
        public PlanetPrefabInfoPair[] PlanetPrefabs()
        {
            return serializedPlanetPrefabs.Split(';').Select(s => s.Split(',')).Where(split => split.Length == 2).Select(split => new PlanetPrefabInfoPair(split[0], split[1])).ToArray();
        }
        public bool HavePlanetPrefabs(string planetPrefabName)
        {
            return PlanetPrefabs().Any(a => a.PlanetPrefabName == planetPrefabName);
        }
        public string PlanetPrefabsPath(string planetPrefabName)
        {
            return PlanetPrefabs().First(c => c.PlanetPrefabName == planetPrefabName).PlanetPrefabPath;
        }
        public Dictionary<string, string> PlanetPrefabsDictionary()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (var pair in _planetPrefabs)
            {
                dictionary.Add(pair.PlanetPrefabName, pair.PlanetPrefabPath);
            }
            return dictionary;
        }
    }
}
