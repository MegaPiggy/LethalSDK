using LethalSDK.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace LethalSDK.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Scrap", menuName = "LethalSDK/Scrap")]
    public class Scrap : ScriptableObject
    {
        [Header("Base")]
        public string itemName = string.Empty;
        public int minValue = 0;
        public int maxValue = 0;
        public bool requiresBattery = false;
        public int weight = 0;
        public GameObject prefab;
        [Header("Sounds")]
        public string grabSFX = string.Empty;
        public string dropSFX = string.Empty;
        [Header("Offsets")]
        public float verticalOffset = 0f;
        public Vector3 restingRotation = Vector3.zero;
        public Vector3 positionOffset = Vector3.zero;
        public Vector3 rotationOffset = Vector3.zero;
        [Header("Variants")]
        public Mesh[] meshVariants = new Mesh[0];
        public Material[] materialVariants = new Material[0];
        [Header("Spawn rate")]
        public bool useGlobalSpawnRate = true;
        [Range(0, 100)]
        public int globalSpawnWeight = 0;
        [SerializeField]
        private ScrapSpawnChance[] _perPlanetSpawnWeight = new ScrapSpawnChance[]
        {
            new ScrapSpawnChance("41 Experimentation", 0),
            new ScrapSpawnChance("220 Assurance", 0),
            new ScrapSpawnChance("56 Vow", 0),
            new ScrapSpawnChance("21 Offense", 0),
            new ScrapSpawnChance("61 March", 0),
            new ScrapSpawnChance("85 Rend", 0),
            new ScrapSpawnChance("7 Dine", 0),
            new ScrapSpawnChance("8 Titan", 0)
        };
        [HideInInspector]
        public string serializedData;
        private void OnValidate()
        {
            serializedData = string.Join(";", _perPlanetSpawnWeight.Select(p => $"{p.SceneName},{p.SpawnWeight}"));
        }
        public ScrapSpawnChance[] perPlanetSpawnWeight()
        {
            return serializedData.Split(';').Select(s => s.Split(',')).Where(split => split.Length == 2).Select(split => new ScrapSpawnChance(split[0], int.Parse(split[1]))).ToArray();
        }
    }
}
