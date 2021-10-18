using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace StreamerService
{
    static class Patcher
    {

        public static bool isActive;

        public static void Patch()
        {
            Debug.Log("[Streamer Mode] Starting patch.");
            var harmony = new Harmony("tepel.streamerMode");
            harmony.PatchAll();
            return;
        }
    }


    [HarmonyPatch(typeof(KMAudio), "PlaySoundAtTransform")]
    static class KMAudioPatch
    { 

        static readonly string[] fullDisable = new string[] { "The Jukebox", "The Festive Jukebox" };
        static bool Prefix(string name, Transform transform)
        {
            if (!Patcher.isActive) return true;
            var moduleName = transform.gameObject.GetComponent<KMBombModule>().ModuleDisplayName;
            var shouldEnable = check(name, moduleName);
            if(!shouldEnable)
            {
                Debug.Log($"[Streamer Mode] Disabled {name} from {moduleName} ");
            }
            return shouldEnable;

        }

        static bool check(string soundName, string moduleName)
        {
            switch (moduleName)
            {
                case "The Jukebox":
                    return soundName == "recordScratch1";
                case "The Festive Jukebox":
                case "Weird Al Yankovic":
                    return soundName == "scratch" || soundName == "winScratch" || soundName == "oldRecord";
                default:
                    return true;
            }
        }
    }
}
