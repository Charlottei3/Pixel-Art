using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RememberBook : MonoBehaviour
{
    public List<int> id1;
    public GameObject book1;

    bool Check(List<int> input, List<int> nowInput)
    {
        if (input.Count == nowInput.Count)
        {
            for (int i = 0; i < input.Count; i++)
            {
                if (input[i] != nowInput[i])
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}
