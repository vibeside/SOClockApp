using MelonLoader;
using ScheduleOne.DevUtilities;
using ScheduleOne.UI;
using ScheduleOne.UI.Phone;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SOClockApp
{
    public class ClockApp : App<ClockApp>
    {
        public List<TimerEntry> entries = new List<TimerEntry>();
        public GUIStyle style = new GUIStyle();
        public GameObject Content;

        public static GameObject entryPrefab = Plugin.clockBundle.LoadAsset<GameObject>("Assets/ClockEntry.prefab");

        public override void Awake()
        {
            AppName = "Clock";
            IconLabel = "Clock";
            AppIcon = Plugin.LoadImage("CLOCKFACE.png");
            AppIcon.name = "Clock";
            Orientation = App<ClockApp>.EOrientation.Vertical;
            AvailableInTutorial = true;
            Content = transform.GetComponentInChildren<HorizontalLayoutGroup>().gameObject;
            base.Awake();
        }

        public override void Start()
        {
            base.Start();
            style.fontSize = 20;
            style.normal.textColor = Color.white;

        }
        public override void Update()
        {
            base.Update();
            for (int i = 0; i < entries.Count; i++)
            {
                entries[i].RemoveIfNull();
            }
            PopulateEntriesAndUpdate();
        }
        public void PopulateEntriesAndUpdate()
        {
            foreach (TimerEntry entry in entries)
            {
                if (entry.entryObject == null)
                {
                    entry.entryObject = Instantiate(Plugin.clockBundle.LoadAsset<GameObject>("Assets/ClockEntry.prefab"), Content.transform);
                    entry.entryText = entry.entryObject.GetComponentInChildren<Text>();
                    //entry.entryText.text = "test";
                }
                if (entry.entryText != null)
                {

                    if (entry.Alarm != null) entry.entryText.text = $"{entry.Name}\n{entry.Station}\n{entry.Alarm.ScreenText.text}";
                    if (entry.Oven != null)
                    {
                        int b = 0;
                        int num = 0;
                        if (entry.Oven.CurrentOperation != null)
                        {
                            b = entry.Oven.CurrentOperation.GetCookDuration() - entry.Oven.CurrentOperation.CookProgress;
                            b = Mathf.Max(0, b);
                            num = b / 60;
                            b %= 60;
                        }
                        entry.entryText.text = $"{entry.Name}\n{entry.Station}\n{num:D2}:{b:D2}";
                    }
                    if (entry.Mk2station != null)
                    {
                        if (entry.Mk2station.CurrentMixOperation != null)
                        {
                            entry.entryText.text = $"{entry.Name}\n{entry.Station}\n00:{entry.Mk2station.GetMixTimeForCurrentOperation() - entry.Mk2station.CurrentMixTime}";
                        }
                        entry.entryText.text = $"{entry.Name}\n{entry.Station}\n00:00";
                    }
                }
            }
        }
    }
    //public void OnGUI()
    //{
    //    if (PlayerSingleton<Phone>.Instance != null)
    //    {
    //        if (PlayerSingleton<Phone>.Instance.IsOpen)
    //        {
    //            for (int i = 0; i < entries.Count; i++)
    //            {
    //                GUI.Label(new Rect(0, i * 100, 100, 100), $"{entries[i].Name}\n{entries[i].Station}\n{entries[i].StationAlarm.text}", style);
    //            }
    //        }
    //    }
    //}

}

