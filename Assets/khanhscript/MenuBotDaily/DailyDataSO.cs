using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "DailyDataSO", menuName = "ScriptTableObject/DailySO")]
public class DailyDataSO : ScriptableObject
{
    
    public DailyDataSO Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }else
            {
                instance = Resources.Load("DailyDataSO") as DailyDataSO;
                return instance;
            }
        }
    }
    static DailyDataSO instance;

    public List<DailyData> dailyData;
}
[Serializable]
public class DailyData
{
    [SerializeField] Sprite sprites ;

    public Sprite Image
    {
        get
        {
           return sprites;
        }
    }
}