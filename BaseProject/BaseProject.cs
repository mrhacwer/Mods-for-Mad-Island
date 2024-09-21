using System;
using MelonLoader;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace BaseProjectClean //Namespace of your project
{
    public class BaseProject : MelonMod
    {
        //Updates every frame.
        public override void OnUpdate()
        {
        }
        //Updates on any scene load.
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
        }
        //Updates on any scene deload.
        public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
        {
        }
    }
}