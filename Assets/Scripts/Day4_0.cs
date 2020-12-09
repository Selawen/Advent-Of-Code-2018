using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Day4_0 : MonoBehaviour
{
    [SerializeField] private TextAsset input;

    private int guardID;
    private int optimalMinute;

    //maybe make month and date 1 int using mod100 (maybe add hour & minute too)
    private DateTime timeStamp;
    private DateTime date;
    
    private Dictionary<DateTime, int> guardAtDate;
    private Dictionary<DateTime, bool?[]> sleptMinutesOnDate; //maybe use bitshift enum (int64)
    private Dictionary<DateTime, bool?[]> sleptAtMinutesOnDate; //maybe use bitshift enum (int64)
    private Dictionary<int, int> sleptMinutesPerGuard; //maybe use bitshift enum (int64)
    private bool?[] sleptMinutes;

    private bool? sleeping;
    private int[] timesSleptAtMinute;

    private TimeSpan timeSpan = TimeSpan.FromDays(1);

    // Start is called before the first frame update
    void Start()
    {
        DateTime sample = DateTime.Now;

        guardAtDate = new Dictionary<DateTime, int>();
        sleptMinutesPerGuard = new Dictionary<int, int>();
        sleptMinutesOnDate = new Dictionary<DateTime, bool?[]>();
        sleptAtMinutesOnDate = new Dictionary<DateTime, bool?[]>();

        //string patternDate = @"\[\d{4}\-(\d{2})\-(\d{2})\s?(\d{2})\:(\d{2})\](.*)";
        string patternDate = @"\[(\d{4}\-\d{2}\-\d{2}\s?\d{2}:\d{2})\](.*)";
        string patternGuardID = @"#(\d+)";
        string patternSleep = @"falls";
        string patternAwaken = @"wakes";

        foreach (System.Text.RegularExpressions.Match m in
        System.Text.RegularExpressions.Regex.Matches(input.text, patternDate))
        {
            //date = new DateTime(1518, int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value), int.Parse(m.Groups[3].Value), int.Parse(m.Groups[4].Value), 0);
            timeStamp = DateTime.Parse(m.Groups[1].Value);

            if (System.Text.RegularExpressions.Regex.IsMatch(m.Groups[2].ToString(), patternGuardID))
            {
                if (timeStamp.Hour == 23)
                {
                    timeStamp = new DateTime((timeStamp.Ticks + timeSpan.Ticks - 1) / timeSpan.Ticks * timeSpan.Ticks, timeStamp.Kind); //round to next day
                }
                date = timeStamp.Date;
                System.Text.RegularExpressions.Match idMatch = System.Text.RegularExpressions.Regex.Match(m.Groups[2].ToString(), patternGuardID);
                int thisGuard = int.Parse(idMatch.Groups[1].Value);
                guardAtDate.Add(date, thisGuard);
                if (!sleptMinutesPerGuard.ContainsKey(thisGuard))
                {
                    sleptMinutesPerGuard.Add(thisGuard, 0);
                }
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(m.Groups[2].ToString(), patternSleep))
            {
                date = timeStamp.Date;
                if (!sleptMinutesOnDate.TryGetValue(date, out sleptMinutes))
                {
                    sleptMinutes = new bool?[60];
                    sleptMinutesOnDate.Add(date, sleptMinutes);
                }

                sleptMinutes[timeStamp.Minute] = true;
                sleptMinutesOnDate[date] = sleptMinutes;
            }
            else if (System.Text.RegularExpressions.Regex.IsMatch(m.Groups[2].ToString(), patternAwaken))
            {
                date = timeStamp.Date;
                if (!sleptMinutesOnDate.TryGetValue(date, out sleptMinutes))
                {
                    sleptMinutes = new bool?[60];
                    sleptMinutesOnDate.Add(date, sleptMinutes);
                }

                sleptMinutes[timeStamp.Minute] = false;
                sleptMinutesOnDate[date] = sleptMinutes;
            }
        }

        foreach (KeyValuePair<DateTime, bool?[]> kvp in sleptMinutesOnDate)
        {
            sleeping = false;
            sleptMinutes = kvp.Value;

            int thisGuard = guardAtDate[kvp.Key];

            for (int i = 0; i < 60; i++)
            {
                if (sleptMinutes[i] == null)
                {
                    sleptMinutes[i] = sleeping;
                }
                else
                {
                    sleeping = sleptMinutes[i];
                }

                if ((bool)sleeping)
                {
                    sleptMinutesPerGuard[thisGuard]++;
                }
            }

            sleptAtMinutesOnDate.Add(kvp.Key, sleptMinutes);
        }


        foreach (KeyValuePair<int, int> kvp in sleptMinutesPerGuard)
        {
            if (guardID == 0)
            {
                guardID = kvp.Key;
            }
            else if (kvp.Value > sleptMinutesPerGuard[guardID])
            {
                guardID = kvp.Key;
            }
        }

        timesSleptAtMinute = new int[60];
        foreach (KeyValuePair<DateTime, int> kvp in guardAtDate)
        {
            if (kvp.Value == guardID)
            {
                sleptMinutes = sleptAtMinutesOnDate[kvp.Key];
                for (int i = 0; i < 60; i++)
                {
                    if (sleptMinutes[i] == true)
                    {
                        timesSleptAtMinute[i]++;
                    }
                }
            }
        }

        int maxTimesSlept = 0;
        for (int i = 0; i < 60; i++)
        { 
            if (timesSleptAtMinute[i] > maxTimesSlept)
            {
                maxTimesSlept = timesSleptAtMinute[i];
                optimalMinute = i;
            }
        }

        //Debug.Log("guard: " + guardID);
        //Debug.Log("minute: " + optimalMinute);
        //Debug.Log("times slept: " + maxTimesSlept);
        Debug.Log("guard# x minute: " + (guardID * optimalMinute));

        TimeSpan elapsed = (DateTime.Now - sample);
        Debug.Log(string.Format("This took {0} milliseconds", elapsed.TotalMilliseconds));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
