using DunGen.Adapters;
using DunGen;
using GameNetcodeStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using LethalSDK.Utils;
using UnityEditor.SearchService;
using UnityEditor;

namespace LethalSDK.Component
{
    public class ScriptImporter : MonoBehaviour
    {
        public virtual void Awake()
        {
            Destroy(this);
        }
    }
    [AddComponentMenu("LethalSDK/MatchLocalPlayerPosition")]
    public class SI_MatchLocalPlayerPosition : ScriptImporter
    {
        public override void Awake()
        {
            this.gameObject.AddComponent<MatchLocalPlayerPosition>();
            base.Awake();
        }
    }
    [AddComponentMenu("LethalSDK/AnimatedSun")]
    public class SI_AnimatedSun : ScriptImporter
    {
        public Light indirectLight;
        public Light directLight;
        public override void Awake()
        {
            var tmp = this.gameObject.AddComponent<animatedSun>();
            tmp.indirectLight = indirectLight;
            tmp.directLight = directLight;
            base.Awake();
        }
    }
    [AddComponentMenu("LethalSDK/ScanNode")]
    public class SI_ScanNode : ScriptImporter
    {
        public int MinRange;
        public int MaxRange;
        public bool RequiresLineOfSight;
        public string HeaderText;
        public string SubText;
        public int ScrapValue;
        public int CreatureScanID;
        public int NodeType;
        public override void Awake()
        {
            var tmp = this.gameObject.AddComponent<ScanNodeProperties>();
            tmp.minRange = MinRange;
            tmp.maxRange = MaxRange;
            tmp.requiresLineOfSight = RequiresLineOfSight;
            tmp.headerText = HeaderText;
            tmp.subText = SubText;
            tmp.scrapValue = ScrapValue;
            tmp.creatureScanID = CreatureScanID;
            tmp.nodeType = NodeType;
            base.Awake();
        }
    }
    [AddComponentMenu("LethalSDK/AudioReverbPresets")]
    public class SI_AudioReverbPresets : ScriptImporter
    {
        public GameObject[] presets;
        public override void Awake()
        {
            return;
        }
        public void Update()
        {
            int i = 0;
            foreach (var preset in presets)
            {
                if(preset.GetComponent<SI_AudioReverbTrigger>() != null)
                {
                    i++;
                }
            }
            if(i == 0)
            {
                var list = new List<AudioReverbTrigger>();
                foreach (var preset in presets)
                {
                    if (preset.GetComponent<AudioReverbTrigger>() != null)
                    {
                        list.Add(preset.GetComponent<AudioReverbTrigger>());
                    }
                }
                var tmp = this.gameObject.AddComponent<AudioReverbPresets>();
                tmp.audioPresets = list.ToArray();
                Destroy(this);
            }
        }
    }
    [AddComponentMenu("LethalSDK/AudioReverbTrigger")]
    public class SI_AudioReverbTrigger : ScriptImporter
    {
        [Header("Reverb Preset")]
        public bool ChangeDryLevel = false;
        [Range(-10000f, 0f)]
        public float DryLevel = 0f;
        public bool ChangeHighFreq = false;
        [Range(-10000f, 0f)]
        public float HighFreq = -270f;
        public bool ChangeLowFreq = false;
        [Range(-10000f, 0f)]
        public float LowFreq = -244f;
        public bool ChangeDecayTime = false;
        [Range(0f, 35f)]
        public float DecayTime = 1.4f;
        public bool ChangeRoom = false;
        [Range(-10000f, 0f)]
        public float Room = -600f;
        [Header("MISC")]
        public bool ElevatorTriggerForProps = false;
        public bool SetInElevatorTrigger = false;
        public bool IsShipRoom = false;
        public bool ToggleLocalFog = false;
        public float FogEnabledAmount = 10f;
        [Header("Weather and effects")]
        public bool SetInsideAtmosphere = false;
        public bool InsideLighting = false;
        public int WeatherEffect = -1;
        public bool EffectEnabled = true;
        public bool DisableAllWeather = false;
        public bool EnableCurrentLevelWeather = true;

        public override void Awake()
        {
            var tmp = this.gameObject.AddComponent<AudioReverbTrigger>();
            ReverbPreset tmppreset = ScriptableObject.CreateInstance<ReverbPreset>();
            tmppreset.changeDryLevel = ChangeDryLevel;
            tmppreset.dryLevel = DryLevel;
            tmppreset.changeHighFreq = ChangeHighFreq;
            tmppreset.highFreq = HighFreq;
            tmppreset.changeLowFreq = ChangeLowFreq;
            tmppreset.lowFreq = LowFreq;
            tmppreset.changeDecayTime = ChangeDecayTime;
            tmppreset.decayTime = DecayTime;
            tmppreset.changeRoom = ChangeRoom;
            tmppreset.room = Room;
            tmp.reverbPreset = tmppreset;
            tmp.usePreset = -1;
            tmp.audioChanges = new switchToAudio[0];
            tmp.elevatorTriggerForProps = ElevatorTriggerForProps;
            tmp.setInElevatorTrigger = SetInElevatorTrigger;
            tmp.isShipRoom = IsShipRoom;
            tmp.toggleLocalFog = ToggleLocalFog;
            tmp.fogEnabledAmount = FogEnabledAmount;
            tmp.setInsideAtmosphere = SetInsideAtmosphere;
            tmp.insideLighting = InsideLighting;
            tmp.weatherEffect = WeatherEffect;
            tmp.effectEnabled = EffectEnabled;
            tmp.disableAllWeather = DisableAllWeather;
            tmp.enableCurrentLevelWeather = EnableCurrentLevelWeather;

            base.Awake();
        }
    }
    [AddComponentMenu("LethalSDK/DungeonGenerator")]
    public class SI_DungeonGenerator : ScriptImporter
    {
        public GameObject DungeonRoot;

        public override void Awake()
        {
            if (this.tag != "DungeonGenerator")
            {
                this.tag = "DungeonGenerator";
            }
            RuntimeDungeon runtimeDungeon = this.gameObject.AddComponent<RuntimeDungeon>();
            runtimeDungeon.Generator.DungeonFlow = RoundManager.Instance.dungeonFlowTypes[0];
            runtimeDungeon.Generator.LengthMultiplier = 0.8f;
            runtimeDungeon.Generator.PauseBetweenRooms = 0.2f;
            runtimeDungeon.GenerateOnStart = false;
            if(DungeonRoot != null && DungeonRoot.scene == null)
            {
                DungeonRoot = new GameObject();
                DungeonRoot.name = "DungeonRoot";
                DungeonRoot.transform.position = new Vector3(0, -200, 0);
            }
            runtimeDungeon.Root = DungeonRoot;
            runtimeDungeon.Generator.DungeonFlow = RoundManager.Instance.dungeonFlowTypes[0];
            UnityNavMeshAdapter dungeonNavMesh = this.gameObject.AddComponent<UnityNavMeshAdapter>();
            dungeonNavMesh.BakeMode = UnityNavMeshAdapter.RuntimeNavMeshBakeMode.FullDungeonBake;
            dungeonNavMesh.LayerMask = 35072; //256 + 2048 + 32768 = 35072

            base.Awake();
        }
    }
    [AddComponentMenu("LethalSDK/EntranceTeleport")]
    public class SI_EntranceTeleport : ScriptImporter
    {
        public int EntranceID = 0;
        public Transform EntrancePoint;
        public int AudioReverbPreset = -1;

        public void Update()
        {
            GameObject dungeonEntrance = GameObject.Find("EntranceTeleportA(Clone)");
            if (dungeonEntrance != null)
            {
                var audioSource = this.gameObject.AddComponent<AudioSource>();
                audioSource.outputAudioMixerGroup = dungeonEntrance.GetComponent<AudioSource>().outputAudioMixerGroup;
                audioSource.playOnAwake = false;
                audioSource.spatialBlend = 1f;
                var entranceTeleport = this.gameObject.AddComponent<EntranceTeleport>();
                entranceTeleport.isEntranceToBuilding = true;
                entranceTeleport.entrancePoint = EntrancePoint;
                entranceTeleport.entranceId = EntranceID;
                entranceTeleport.audioReverbPreset = AudioReverbPreset;
                entranceTeleport.entrancePointAudio = audioSource;
                entranceTeleport.doorAudios = dungeonEntrance.GetComponent<EntranceTeleport>().doorAudios;
                var trigger = this.gameObject.AddComponent<InteractTrigger>();
                trigger.hoverIcon = dungeonEntrance.GetComponent<InteractTrigger>().hoverIcon;
                trigger.hoverTip = "Enter : [LMB]";
                trigger.interactable = true;
                trigger.oneHandedItemAllowed = true;
                trigger.twoHandedItemAllowed = true;
                trigger.holdInteraction = true;
                trigger.timeToHold = 1.5f;
                trigger.timeToHoldSpeedMultiplier = 1f;
                trigger.holdingInteractEvent = new InteractEventFloat();
                trigger.onInteract = new InteractEvent();
                trigger.onInteractEarly = new InteractEvent();
                trigger.onStopInteract = new InteractEvent();
                trigger.onCancelAnimation = new InteractEvent();
                trigger.onInteract.AddListener((player) => entranceTeleport.TeleportPlayer());

                Destroy(this);
            }
        }
        public override void Awake()
        {
            return;
        }
    }
    [AddComponentMenu("LethalSDK/DoorLock")]
    public class SI_DoorLock : ScriptImporter
    {
        public override void Awake()
        {
            base.Awake();
        }
    }
    [AddComponentMenu("LethalSDK/WaterSurface")]
    public class SI_WaterSurface : ScriptImporter
    {
        GameObject obj;
        public override void Awake()
        {
            obj = Instantiate(SpawnPrefab.Instance.waterSurface);
            SceneManager.MoveGameObjectToScene(obj, this.gameObject.scene);
            obj.transform.parent = this.transform;
            obj.transform.localPosition = Vector3.zero;
            obj.SetActive(true);
        }
    }
    [AddComponentMenu("LethalSDK/Ladder")]
    public class SI_Ladder : ScriptImporter
    {
        public override void Awake()
        {
            base.Awake();
        }
    }
    [AddComponentMenu("LethalSDK/ItemDropship")]
    public class SI_ItemDropship : ScriptImporter
    {
        public Animator ShipAnimator;
        public Transform[] ItemSpawnPositions;
        public GameObject OpenTriggerObject;
        public GameObject KillTriggerObject;
        public AudioClip ShipThrusterCloseSound;
        public AudioClip ShipLandSound;
        public AudioClip ShipOpenDoorsSound;
        public override void Awake()
        {
            var ItemDropship = this.gameObject.AddComponent<ItemDropship>();
            ItemDropship.shipAnimator = ShipAnimator;
            ItemDropship.itemSpawnPositions = ItemSpawnPositions;

            var _PlayAudioAnimationEvent = this.gameObject.AddComponent<PlayAudioAnimationEvent>();
            _PlayAudioAnimationEvent.audioToPlay = this.GetComponent<AudioSource>();
            _PlayAudioAnimationEvent.audioClip = ShipLandSound;
            _PlayAudioAnimationEvent.audioClip2 = ShipOpenDoorsSound;

            var OpenTriggerScript = OpenTriggerObject.AddComponent<InteractTrigger>();
            OpenTriggerScript.hoverTip = "Open : [LMB]";
            OpenTriggerScript.twoHandedItemAllowed = true;
            OpenTriggerScript.onInteract = new InteractEvent();
            OpenTriggerScript.onInteract.AddListener((player) => ItemDropship.TryOpeningShip());

            ItemDropship.triggerScript = OpenTriggerScript;

            ItemDropship.transform.Find("Music").gameObject.AddComponent<OccludeAudio>();
            var FacePlayerOnAxis = this.transform.Find("ThrusterContainer/Thruster").gameObject.AddComponent<facePlayerOnAxis>();
            FacePlayerOnAxis.turnAxis = this.transform.Find("ThrusterContainer/flameAxis");

            var KillLocalPlayerScript = KillTriggerObject.AddComponent<KillLocalPlayer>();

            var KillTriggerScript = KillTriggerObject.AddComponent<InteractTrigger>();
            KillTriggerScript.touchTrigger = true;
            KillTriggerScript.triggerOnce = true;
            KillTriggerScript.onInteract = new InteractEvent();
            KillTriggerScript.onInteract.AddListener((player) => KillLocalPlayerScript.KillPlayer(player));
        }

        public void Update()
        {
            GameObject dungeonEntrance = GameObject.Find("EntranceTeleportA(Clone)");
            if (dungeonEntrance != null)
            {
                var TriggerScript = OpenTriggerObject.GetComponent<InteractTrigger>();
                TriggerScript.hoverIcon = dungeonEntrance.GetComponent<InteractTrigger>().hoverIcon;
                Destroy(this);
            }
        }
    }
}
