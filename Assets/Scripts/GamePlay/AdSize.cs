using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdSize : MonoBehaviour
{
    private void Start()
    {
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width - 40, Screen.height / 3);
    }
}
