using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSize : MonoBehaviour
{
    public GridLayoutGroup gridLayoutGroup;
    private void Start()
    {
        gridLayoutGroup.cellSize = new Vector2((Screen.width - 60) / 2, (Screen.width - 60) / 2);
    }
}
