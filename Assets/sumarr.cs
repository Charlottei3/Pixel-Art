using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sumarr : MonoBehaviour
{
    [SerializeField] int[] arr;
    [SerializeField] int target;
    bool isBool = false;
    private void Start()
    {
        sum();
    }
    public void sum()
    {
        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = i + 1; j < arr.Length; j++)
            {
                if (arr[i] + arr[j] == target)
                {
                    Debug.Log($"[{arr[i]}], [{arr[j]}]");
                    isBool = true;
                    break;
                }
                
            }
            if (isBool == true) break;
        }
    }
}
