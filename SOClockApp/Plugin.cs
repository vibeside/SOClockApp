using MelonLoader;
using Microsoft.Diagnostics.Runtime.AbstractDac;
using MonoMod;
using MonoMod.RuntimeDetour;
using ScheduleOne.DevUtilities;
using ScheduleOne.ObjectScripts;
using ScheduleOne.UI.Phone;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Yoga;

namespace SOClockApp
{
    //D:\SteamLibrary\steamapps\common\Schedule I\Mods\paca-SOClockApp

    public class Plugin : MelonMod
    {

        public static HarmonyLib.Harmony harmony;


        public static bool appUpdate = false;


        public static AssetBundle clockBundle;


        public static UnityEngine.Object test;

        public static GameObject mainCanvasPrefab;


        public override void OnInitializeMelon()
        {
            base.OnInitializeMelon();
            MelonLogger.Msg("Clock app loaded!");
            harmony = new HarmonyLib.Harmony("com.coolpaca.clockapp");
            string sAssemblyLocation = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            clockBundle = AssetBundle.LoadFromFile(Path.Combine(sAssemblyLocation, "clockbundle"));

            MelonCoroutines.Start(WaitForPhone());
            harmony.PatchAll();

           
        }
        public static IEnumerator WaitForPhone()
        {
            while(PlayerSingleton<AppsCanvas>.Instance == null)
            {
                yield return null;
            }
            AppsCanvas appcanvas = PlayerSingleton<AppsCanvas>.Instance;
            mainCanvasPrefab = GameObject.Instantiate(clockBundle.LoadAsset<GameObject>("Assets/Clock.prefab"),appcanvas.canvas.transform);
            ClockApp app = mainCanvasPrefab.AddComponent<ClockApp>();
            app.appContainer = mainCanvasPrefab.transform.GetChild(0).GetComponent<RectTransform>();
        }


        public static Sprite LoadImage(string Name)
        {
            string imgPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\" + Name;
            byte[] imgData = File.ReadAllBytes(imgPath);
            Texture2D tex = new Texture2D(2, 2);
            if (!tex.LoadImage(imgData))
            {
                MelonLogger.Error($"Failed to load image. Make sure its named \"{Name}\" exactly.");
                return null;
            }
            else
            {
                Sprite icon = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one / 2);
                return icon;
                // set app icon
            }
        }
    }
}
