using System;
using MelonLoader;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BaseProjectClean //Namespace of your project
{
    public class BaseProjectHarmony : MelonMod
    {
        [HarmonyPatch(typeof(PlayerMove), "Update")]
        class BaseProjectCleam : HarmonyPatch
        {
            public static void Postfix(PlayerMove __instance)
            {
                {
                    //RUN CODE
                }
            }
        }
    }
}