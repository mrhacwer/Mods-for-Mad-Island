using MelonLoader;
using HarmonyLib;
using UnityEngine;

namespace MoreZoom
{
    public class MoreZoomMOD : MelonMod
    {
        [HarmonyPatch(typeof(FollowTarget), "Start")]
        class patchCamera : HarmonyPatch
        {
            public static void Postfix(FollowTarget __instance)
            {
                {
                    __instance.moveMin *= 2f;
                    __instance.zoom.transform.Find("Main Camera").GetComponent<Camera>().farClipPlane *= 2f;
                }
            }
        }
        [HarmonyPatch(typeof(CullDistance), "Start")]
        class patchCullDistance : HarmonyPatch
        {
            public static void Prefix(CullDistance __instance)
            {
                for (int i = 0; i < __instance.cullDistance.Length; i++)
                {
                    __instance.cullDistance[i] *= 2f;
                }
            }
        }
    }
}