using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentManager : MonoBehaviour
{
    [SerializeField] List<GameObject> listObj = new List<GameObject>();
    [SerializeField] List<GameObject> _childContent = new List<GameObject>();
    [SerializeField] List<bool> isOpen = new List<bool>();
    
    private int currentMonth;

    private void Start()
    {
        DateTime now = DateTime.Now;
        currentMonth = now.Month;

        for (int i = 0; i < listObj.Count; i++)
        {
           
            if (i < currentMonth)
            {
                listObj[i].gameObject.SetActive(false);
                continue;
            }
        }
    }

    private void ShowMonth(int month)
    {
        isOpen[month] = !isOpen[month];

        if (isOpen[month])
        {
            _childContent[month].gameObject.SetActive(true);
        }
        else
        {
            _childContent[month].gameObject.SetActive(false);
        }
        
    }
    
   
    public void BtnOnclick(int month)
    {
       ShowMonth(month);
    }
}
