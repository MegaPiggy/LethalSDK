using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;

namespace LethalSDK.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Moon", menuName = "LethalSDK/Moon")]
    public class Moon : ScriptableObject
    {
        public string MoonName;

        [Header("Info")]
        public string OrbitPrefabName = "Moon1";
        public bool SpawnEnemiesAndScrap = true;
        public string PlanetName = "New Moon";
        public GameObject MainPrefab;
        [TextArea(5, 15)]
        public string PlanetDescription;
        public VideoClip PlanetVideo;
        public string RiskLevel = "X";
        public float TimeToArrive = 1;

        [Header("Time")]
        public float DaySpeedMultiplier = 1f;
        public bool PlanetHasTime = true;

        [Header("Route")]
        public string RouteWord = "newmoon";
        public int RoutePrice;
        public string BoughtComment = "Please enjoy your flight.";

        /*[Header("Dungeon")]
        [Header("Outside")]*/
    }
}
