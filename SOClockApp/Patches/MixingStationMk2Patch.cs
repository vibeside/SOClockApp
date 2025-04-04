using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using MelonLoader;
using ScheduleOne.DevUtilities;
using ScheduleOne.ObjectScripts;
using ScheduleOne.Property;
using UnityEngine;


namespace SOClockApp.Patches
{
    [HarmonyPatch(typeof(MixingStation),"InitializeGridItem")]
    internal class MixingStationMk2Patch
    {
        static bool Add = true;
        public static void Postfix(MixingStationMk2 __instance)
        {
            if (__instance.TryGetComponent(out MixingStationMk2 mk2))
            {
                TimerEntry entry = new TimerEntry(mk2.GetProperty(mk2.OwnerGrid.transform).propertyName, "Mixing Station Mk2", mk2.gameObject.GetInstanceID(), mk2.gameObject, mk2);
                foreach (var e in PlayerSingleton<ClockApp>.Instance.entries)
                {
                    if (entry.GUID == e.GUID) Add = false;
                }
                if (Add)
                {
                    PlayerSingleton<ClockApp>.Instance.entries.Add(entry);
                }
                Add = true;
            }
        }
    }
}
