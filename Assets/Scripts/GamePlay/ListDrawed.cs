using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ListDrawed : MonoBehaviour
{
    public List<Btn_loadGame1> listDrawed;
    public List<Btn_loadGame1> listDrawing;
    public Transform saveDrawed;
    public Transform saveDrawing;
    private void Awake()
    {
        //Load data
        //listDrawed = Data.GetListDrawed();
        //listDrawing = Data.GetListDrawing();
    }
    private void Start()
    {
        //add btn
        foreach (var btn in listDrawed)
        {
            Btn_loadGame1 copy = Instantiate(btn);
            copy.transform.parent = saveDrawed;
        }
        foreach (var btn in listDrawing)
        {
            Btn_loadGame1 copy = Instantiate(btn);
            copy.transform.parent = saveDrawing;
        }
    }
}
