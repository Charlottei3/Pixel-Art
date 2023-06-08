using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roman : MonoBehaviour
{
    public string str;
    Dictionary<char, int> library = new Dictionary<char, int>()
    {
        {'I',1 },
        {'V',5 },
        {'X',10 },

        {'L',50 },
        {'C',100 },

        {'D',500 },
        {'M',1000 }


    };
    private void Start()
    {

        Debug.Log(Count(Check(Read(str))));
    }
    int[] Read(string str)
    {
        int[] save = new int[str.Length];
        string t = string.Empty;
        for (int i = 0; i < str.Length; i++)
        {
            save[i] = library[str[i]];
            string g = i == str.Length - 1 ? "" : "|";
            t += $"{save[i]} {g} ";

        }

        Debug.Log($"Read: {t}");
        return save;
    }

    int[] Check(int[] input)
    {
        int[] aftercheck = input;
        string t = string.Empty;

        for (int i = 0; i < aftercheck.Length - 1; i++)
        {
            if (aftercheck[i] < aftercheck[i + 1])
            {
                aftercheck[i] = -aftercheck[i];
            }
        }

        for (int i = 0; i < aftercheck.Length; i++)
        {
            string g = i == aftercheck.Length - 1 ? "" : "+";
            t += $"[{aftercheck[i]}] {g} ";
        }

        Debug.Log($"Check: {t}");
        return aftercheck;
    }
    int Count(int[] aray)
    {
        int sum = 0;
        for (int i = 0; i < aray.Length; i++)
        {
            sum += aray[i];
        }
        return sum;
    }

}
