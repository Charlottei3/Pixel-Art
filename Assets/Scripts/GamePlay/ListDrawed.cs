using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ListDrawed : MonoBehaviour
{
    public Dictionary<Btn_loadGame1, Btn_loadGame1> DictionaryCopyOnDrawed = new Dictionary<Btn_loadGame1, Btn_loadGame1>();

    public Transform saveDrawing;
    public Transform saveComplete;
    private void Awake()
    {
        //Load data
        //listDrawed = Data.GetListDrawed();
        //listDrawing = Data.GetListDrawing();
    }
    private void Start()
    {

    }
    public void AddBtnLoad(Btn_loadGame1 Btn_input, Transform parent)
    {
        Btn_loadGame1 copy = Instantiate(Btn_input);
        copy.isInDrawed = true;
        copy.name = Btn_input.key;
        copy.transform.SetParent(parent);
        DictionaryCopyOnDrawed.Add(copy, Btn_input);
        copy.transform.localScale = Vector3.one;
    }
    public void RemoveToCompelete(Btn_loadGame1 Btn_input)
    {
        Transform find = saveDrawing.Find(Btn_input.gameObject.name);
        if (find == null) return;
        else
            find.SetParent(saveComplete);

    }
}
