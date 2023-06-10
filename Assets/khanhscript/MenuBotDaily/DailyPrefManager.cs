using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class DailyPrefManager : MonoBehaviour
{
    private static DailyPrefManager _instace;
    public static DailyPrefManager Instace { get { return _instace; } }
    [SerializeField] public DailyDataSO dailyDataSO,whileBlackSO;
    private void Awake()
    {
        _instace = this;

    }
    [Header("------Item in Month-----")]
    [SerializeField] private DayItem _prefabs;
    [SerializeField] private List<Transform> parent;


    [Header("------Month in Year-----")]
    [SerializeField] private GameObject _prefabMonth;
    [SerializeField] private Transform parentMonth;
    [SerializeField] private TMP_Text _tMonth;


    private int MonthInYear;
    private int currentDay;
    private void Start()
    {
        DateTime now = DateTime.Now;
        MonthInYear = now.Month;
        currentDay = now.Day;
        _tMonth.text = now.ToString("MMMM");
        SpawnDayMonth();
    }

    public void SpawnDayMonth()
    {
        for (int i = currentDay; i > 0; i--)
        {
            var _Item = Instantiate(_prefabs, parent[0]);
            _Item.Text = i.ToString();

            if (i == currentDay )
            {
                _Item.gameObject.SetActive(false);
                continue;
            }
            _Item.Image = dailyDataSO.dailyData[i].Image;
           
            Btn_loadGame1.Instance.loadPicture.GetComponent<Image>().sprite = whileBlackSO.dailyData[i].Image;
        }
    }
#if all_dayinyear
    public void MonthTransform()
    {
        for (int i = 0; i < parent.Count; i++)
        {
            if (i != MonthInYear) continue;
            parent[i].gameObject.SetActive(false);
        }
    }

    
    public void SpawnerDailyItem()
    {
        for (int month = 0; month < parent.Count; month++)
        {
            int daysInCurrentMonth = DateTime.DaysInMonth(DateTime.Now.Year, month + 1);
            if (month >= MonthInYear) break;
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
#endif

}

