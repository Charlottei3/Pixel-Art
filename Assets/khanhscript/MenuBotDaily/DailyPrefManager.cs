using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DailyPrefManager : MonoBehaviour
{
    private static DailyPrefManager _instace;
    public static DailyPrefManager Instace { get { return _instace; } }
    [SerializeField] public DailyDataSO dailyDataSO;
    private void Awake()
    {
        _instace = this;
        DateTime now = DateTime.Now;
        MonthInYear = now.Month;
        currentDay = now.Day;
        SpawnerDailyItem();
        MonthTransform();
    }
    [Header("------Item in Month-----")]
    [SerializeField] private DayItem _prefabs;
    [SerializeField] private List<Transform> parent;


    [Header("------Month in Year-----")]
    [SerializeField] private GameObject _prefabMonth;
    [SerializeField] private Transform parentMonth;
    private int MonthInYear;
    private int currentDay;
    private void Start()
    {
        
    }


    public void MonthTransform()
    {
        for (int i = 0; i < parent.Count; i++)
        {
            if (i < MonthInYear) continue;
            parent[i].gameObject.SetActive(false);
        }
    }

    public void SpawnerDailyItem()
    {
        for (int month = 0; month < parent.Count; month++)
        {
            int daysInCurrentMonth = DateTime.DaysInMonth(DateTime.Now.Year, month + 1);

            for (int day = daysInCurrentMonth; day >= 1; day--)
            {
                var _Item = Instantiate(_prefabs, parent[month]);
                _Item.Text = day.ToString();
                if (day == currentDay && month + 1 == MonthInYear)
                {
                    _Item.gameObject.SetActive(false);
                    continue;
                }

                if (month + 1 == MonthInYear && day > currentDay)
                {
                    _Item.gameObject.SetActive(false);
                }
                else
                {
                    _Item.Image = dailyDataSO.dailyData[day].Image;
                }
            }
        }
    }


}

