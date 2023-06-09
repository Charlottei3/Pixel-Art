using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DailyDayImage : MonoBehaviour
{
    [SerializeField] TMP_Text d_text, _dateIcon;
    [SerializeField] DayItem prefabs;
    [SerializeField] Transform parent;
    private int _day;
    List<DailyData> _dayBanner = new List<DailyData>();
    private void Awake()
    {
        DateTime now = DateTime.Now;
        string dayofWeek = now.DayOfWeek.ToString();
        string dayofMonth = now.Month.ToString();
        string month = now.ToString("MMMM");
        string year = now.Year.ToString();

        string today = string.Format("{0}, {1},{2} Year {3}", dayofWeek, month, dayofMonth, year);
        d_text.text = today;
        _dateIcon.text = now.Day.ToString();
        _day = now.Day;
        SpawnDay();
    }
    private void Start()
    {
       
        for (int i = 0; i < DailyPrefManager.Instace.dailyDataSO.dailyData.Count; i++)
        {
            _dayBanner.Add(DailyPrefManager.Instace.dailyDataSO.dailyData[i]);
        }
    }

    public void SpawnDay()
    {
        for (int i = 0; i < _dayBanner.Count; i++)
        {
            if (i == _day)
            {
                var item = Instantiate(prefabs, parent);
                item.Image = _dayBanner[i].Image;
                continue;
            }
        }
    }

}