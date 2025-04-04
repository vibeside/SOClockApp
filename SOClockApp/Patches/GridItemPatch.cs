using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using ScheduleOne.DevUtilities;
using ScheduleOne.EntityFramework;
using ScheduleOne.ItemFramework;
using ScheduleOne.Misc;
using ScheduleOne.ObjectScripts;
using ScheduleOne.Tiles;
using ScheduleOne.UI.Phone.Messages;
using MelonLoader;
using UnityEngine;
using static MelonLoader.MelonLogger;
using Grid = ScheduleOne.Tiles.Grid;

namespace SOClockApp.Patches
{
    [HarmonyPatch(typeof(DigitalAlarm), "Start")]
    internal static class GridItemPatch
    {
        static bool Add = true;
        public static void Postfix(DigitalAlarm __instance)
        {
            GridItem parent = __instance.GetComponentInParent<GridItem>();
            if (parent != null)
            {
                if (parent.ItemInstance != null)
                {
                    if (parent.ItemInstance.Definition != null)
                    {
                        TimerEntry newTimer = new TimerEntry(parent.GetProperty(parent.OwnerGrid.transform).propertyName, parent.ItemInstance.Definition.ID, parent.GetInstanceID(),parent.gameObject,__instance);
                        foreach(var e in PlayerSingleton<ClockApp>.Instance.entries)
                        {
                            if (newTimer.GUID == e.GUID) Add = false;
                        }
                        if (Add)
                        {
                            PlayerSingleton<ClockApp>.Instance.entries.Add(newTimer);
                        }
                    }
                }
            }
            Add = true;
            //if (instance.definition != null)
            //{
            //    MelonLogger.Msg(instance.definition.ID);
            //    MelonLogger.Msg(__instance.GetProperty(grid.transform).PropertyName);
            //    DigitalAlarm alarm = __instance.GetComponentInChildren<DigitalAlarm>();
            //    StationTimer newTimer = new StationTimer(__instance.GetProperty(grid.transform).propertyName, instance.definition.ID, alarm, GUID);
            //    foreach (var p in Plugin.entries)
            //    {
            //        if (newTimer.GUID == p.GUID)
            //        {
            //            Add = false;
            //        }
            //    }
            //    if (alarm != null && Add)
            //    {
            //        Plugin.entries.Add(newTimer);
            //    }
            //}
        }
    }
}
