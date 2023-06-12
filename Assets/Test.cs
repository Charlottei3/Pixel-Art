using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Test : MonoBehaviour
{
    int[] numbers = { 7, 6, 5, 4, 3, 1, 0 };
    private void Start()
    {
        Debug.Log(Find(numbers, 0, 7));
    }
    private int Find(int[] array, int min, int max)
    {
        for (int i = min; i <= max; i++)
        {
            if (!array.Contains(i))
            {
                return (i);
            }

        }
        return max + 1;

    }

}

public struct Number //[!] class: References type
                     //[!] struct: Value Type
{

}


