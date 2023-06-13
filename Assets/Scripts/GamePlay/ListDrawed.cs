using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ListDrawed : MonoBehaviour
{
    public Dictionary<Btn_loadGame1, Btn_loadGame1> DictionaryCopyOnDrawed = new Dictionary<Btn_loadGame1, Btn_loadGame1>();
    public List<Btn_loadGame1> listDrawing;
    public List<Btn_loadGame1> listComplete;
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
        foreach (var btn in listDrawing)
        {
            AddBtnLoad(btn, saveDrawed);
        }
        foreach (var btn in listComplete)
        {
            AddBtnLoad(btn, saveDrawing);
        }
    }
    public void AddBtnLoad(Btn_loadGame1 Btn_input, Transform parent)
    {
        Btn_loadGame1 copy = Instantiate(Btn_input);
        copy.isInDrawed = true;
        copy.name = Btn_input.key;
        copy.transform.SetParent(parent);
        DictionaryCopyOnDrawed.Add(copy, Btn_input);
    }
    public void RemoveToCompelete(Btn_loadGame1 Btn_input)
    {
        Transform find = saveDrawed.Find(Btn_input.gameObject.name);
        if (find == null) return;
        else
            find.SetParent(saveDrawing);

    }
}
