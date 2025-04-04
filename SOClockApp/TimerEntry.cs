using ScheduleOne.DevUtilities;
using ScheduleOne.Misc;
using ScheduleOne.ObjectScripts;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SOClockApp
{
    public class TimerEntry
    {
        public string Name;
        public string Station;
        public int GUID;
        public GameObject StationObject;

        public GameObject entryObject;
        public Text entryText;

        public DigitalAlarm Alarm;
        public LabOven Oven;
        public MixingStationMk2 Mk2station;


        public TimerEntry(string name, string station, int guid, GameObject stationObject, DigitalAlarm alarm)
        {
            Name = name;
            Station = station;
            GUID = guid;
            StationObject = stationObject;
            Alarm = alarm;
        }
        public TimerEntry(string name, string station, int guid, GameObject stationObject, LabOven oven)
        {
            Name = name;
            Station = station;
            GUID = guid;
            StationObject = stationObject;
            Oven = oven;
        }
        public TimerEntry(string name, string station, int guid, GameObject stationObject, MixingStationMk2 mixing)
        {
            Name = name;
            Station = station;
            GUID = guid;
            StationObject = stationObject;
            Mk2station = mixing;
        }
        public void RemoveIfNull()
        {
            if(Mk2station == null && Alarm == null && Oven == null)
            {
                PlayerSingleton<ClockApp>.Instance.entries.Remove(this);
            } 
        }
    }
}
