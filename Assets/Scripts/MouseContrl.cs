using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class MouseContrl : MonoBehaviour
{
    private GameManager gameManager;
    private void Awake()
    {
    }
    private void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            //Check true false
        }
    }

}
