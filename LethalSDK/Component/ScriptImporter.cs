﻿using GameNetcodeStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

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
    [AddComponentMenu("LethalSDK/ScanNodeEntrance")]
    public class SI_ScanNodeEntrance : ScriptImporter
    {
        public string HeaderText = "Main entrance";
        public override void Awake()
        {
            var tmp = this.gameObject.AddComponent<ScanNodeProperties>();
            tmp.minRange = 17;
            tmp.maxRange = 100;
            tmp.requiresLineOfSight = false;
            tmp.headerText = HeaderText;
            tmp.subText = String.Empty;
            tmp.scrapValue = 0;
            tmp.creatureScanID = -1;
            tmp.nodeType = 0;
            base.Awake();
        }
    }
    [AddComponentMenu("LethalSDK/ScanNodeShip")]
    public class SI_ScanNodeShip : ScriptImporter
    {
        public override void Awake()
        {
            var tmp = this.gameObject.AddComponent<ScanNodeProperties>();
            tmp.minRange = 17;
            tmp.maxRange = 110;
            tmp.requiresLineOfSight = false;
            tmp.headerText = "Ship";
            tmp.subText = "Home base";
            tmp.scrapValue = 0;
            tmp.creatureScanID = -1;
            tmp.nodeType = 0;
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
        [Header("Weather and effects")]
        public bool SetInsideAtmosphere = false;
        public bool InsideLighting = false;
        public bool EffectEnabled = true;
        public bool DisableAllWeather = false;
        public bool EnableCurrentLevelWeather = true;

        public override void Awake()
        {
            var tmp = this.gameObject.AddComponent<AudioReverbTrigger>();
            ReverbPreset tmppreset = (ReverbPreset)ScriptableObject.CreateInstance(typeof(ReverbPreset));
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
            tmp.weatherEffect = 0;
            tmp.audioChanges = new switchToAudio[0];
            tmp.elevatorTriggerForProps = ElevatorTriggerForProps;
            tmp.setInsideAtmosphere = SetInsideAtmosphere;
            tmp.insideLighting = InsideLighting;
            tmp.effectEnabled = EffectEnabled;
            tmp.disableAllWeather = DisableAllWeather;
            tmp.enableCurrentLevelWeather = EnableCurrentLevelWeather;

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
}
