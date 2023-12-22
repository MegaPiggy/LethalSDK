using GameNetcodeStuff;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine;
using Unity.Netcode;
using LethalSDK.Utils;

namespace LethalSDK.Component
{
    [AddComponentMenu("LethalSDK/DamagePlayer")]
    public class SI_DamagePlayer : MonoBehaviour
    {
        public bool kill = false;
        public bool dontSpawnBody = false;

        public SI_CauseOfDeath causeOfDeath = SI_CauseOfDeath.Gravity;

        public int damages = 25;

        public int numberIterations = 1;
        public int iterationCooldown = 1000;
        public int warmupCooldown = 0;

        public UnityEvent postEvent = new UnityEvent();
        public void Trigger(object player)
        {
            if (kill)
            {
                StartCoroutine(Kill(player));
            }
            else
            {
                StartCoroutine(Damage(player));
            }
        }
        public IEnumerator Kill(object player)
        {
            yield return new WaitForSeconds(warmupCooldown / 1000f);
            (player as PlayerControllerB).KillPlayer(Vector3.zero, !dontSpawnBody, (CauseOfDeath)(int)causeOfDeath, 0);
            postEvent.Invoke();
        }
        public IEnumerator Damage(object player)
        {
            yield return new WaitForSeconds(warmupCooldown / 1000f);
            int iteration = 0;
            while (iteration < numberIterations || numberIterations == -1)
            {
                (player as PlayerControllerB).DamagePlayer(damages, true, true, (CauseOfDeath)(int)causeOfDeath, 0, false, Vector3.zero);
                postEvent.Invoke();
                iteration++;
                yield return new WaitForSeconds(iterationCooldown / 1000f);
            }
        }
        public void StopCounter(object player)
        {
            StopAllCoroutines();
        }

    }
    [AddComponentMenu("LethalSDK/SoundYDistance")]
    public class SI_SoundYDistance : MonoBehaviour
    {
        public AudioSource audioSource;
        public int maxDistance = 50;

        public void Awake()
        {
            if (audioSource == null)
            {
                audioSource = this.gameObject.GetComponent<AudioSource>();
                if (audioSource == null)
                {
                    audioSource = this.gameObject.AddComponent<AudioSource>();
                }
            }
        }
        public void Update()
        {
            if (RoundManager.Instance != null && StartOfRound.Instance != null)
            {
                audioSource.volume = 1 - (Mathf.Abs(this.transform.position.y - RoundManager.Instance.playersManager.allPlayerScripts[StartOfRound.Instance.ClientPlayerList[StartOfRound.Instance.NetworkManager.LocalClientId]].gameplayCamera.transform.position.y) / maxDistance);
            }
        }
    }
    [AddComponentMenu("LethalSDK/AudioOutputInterface")]
    public class SI_AudioOutputInterface : MonoBehaviour
    {
        public AudioSource audioSource;
        public string mixerName = "Diagetic";
        public string mixerGroupName = "Master";
        public void Awake()
        {
            if (audioSource == null)
            {
                audioSource = this.gameObject.GetComponent<AudioSource>();
                if (audioSource == null)
                {
                    audioSource = this.gameObject.AddComponent<AudioSource>();
                }
            }
            if(mixerName != null && mixerName.Length > 0 && mixerGroupName != null && mixerGroupName.Length > 0)
            {
                audioSource.outputAudioMixerGroup = AssetGatherDialog.audioMixers[mixerName].Item2.First(g => g.name == mixerGroupName);
            }
            Destroy(this);
        }
    }
}
