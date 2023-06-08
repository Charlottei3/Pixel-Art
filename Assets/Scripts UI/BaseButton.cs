using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class BaseButton : DatMono
{
    [Header("Base Button")]
    [SerializeField] protected Button button;
    public override void Start()
    {
        base.Start();
        button = GetComponent<Button>();
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.AddOnClickEvent();
    }
    protected virtual void LoadButton()

    {
        if (this.button != null) return;
        this.button = GetComponent<Button>();
        Debug.LogWarning(transform.name + ":loadButton", gameObject);

    }
    protected virtual void AddOnClickEvent()
    {
        this.button.onClick.AddListener(this.OnClick);
    }
    protected abstract void OnClick();
}
