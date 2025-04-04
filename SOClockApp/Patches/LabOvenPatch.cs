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
    [HarmonyPatch(typeof(LabOven),"InitializeGridItem")]
    internal class LabOvenPatch
    {
        static bool Add = true;
        public static void Postfix(LabOven __instance)
        {

            TimerEntry entry = new TimerEntry(__instance.GetProperty(__instance.OwnerGrid.transform).propertyName, "Lab Oven", __instance.gameObject.GetInstanceID(), __instance.gameObject, __instance);
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
