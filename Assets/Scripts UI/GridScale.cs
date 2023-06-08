using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridScale : MonoBehaviour
{
    public GridLayoutGroup gridLayoutGroup;
    void Start()
    {
        gridLayoutGroup.cellSize = new Vector2(Screen.width, Screen.height * 19 / 100);
    }
}
