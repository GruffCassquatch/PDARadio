
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using Story;
using BepInEx;
using BepInEx.Logging;

namespace PDARadio
{
    [BepInPlugin(myGUID, pluginName, versionString)]

    public class PDARadio : BaseUnityPlugin
    {
            private const string myGUID = "com.GruffCassquatch.PDARadio";
            private const string pluginName = "PDA Radio";
            private const string versionString = "2.0.0.0";

            private static readonly Harmony harmony = new Harmony(myGUID);

            public static ManualLogSource logger;

            private void Awake()
            {
                harmony.PatchAll();
                Logger.LogInfo(pluginName + " " + versionString + " " + "loaded.");
                logger = Logger;
            }
        }

    [HarmonyPatch(typeof(StoryGoalManager))]
    [HarmonyPatch("InvokePendingMessageEvent")]
    internal class StoryGoalManager_InvokePendingMessageEvent_Patch
    {
        [HarmonyPostfix]
        private static void Postfix()
        {
            if (GameObject.Find("EscapePod").transform.Find("ModulesRoot/RadioRoot/Radio(Clone)").gameObject.GetComponent<LiveMixin>().IsFullHealth())
            {
                StoryGoalManager.main.Invoke("ExecutePendingRadioMessage", 15f);
            }
        }
    }
}
