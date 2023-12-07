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
        private AudioClipInfoPair[] _clips = new AudioClipInfoPair[0];
        [HideInInspector]
        public string serializedData;
        private void OnValidate()
        {
            serializedData = string.Join(";", _clips.Select(p => $"{(p.AudioClipName.Length == 0 ? (p.AudioClip != null ? p.AudioClip.name : "") : p.AudioClipName)},{AssetDatabase.GetAssetPath(p.AudioClip)}"));
        }
        public AudioClipInfoPair[] AudioClips()
        {
            return serializedData.Split(';').Select(s => s.Split(',')).Where(split => split.Length == 2).Select(split => new AudioClipInfoPair(split[0], split[1])).ToArray();
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
            foreach (var pair in _clips)
            {
                dictionary.Add(pair.AudioClipName, pair.AudioClipPath);
            }
            return dictionary;
        }
    }
}
