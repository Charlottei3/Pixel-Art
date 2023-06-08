using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonBase : ButtonBehav
{
    [SerializeField] private Button button;

    protected override void Start()
    {
        base.Start();
        this.AddOnclickEvent();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.AddOnclickEvent();
    }
    protected virtual void LoadButton()
    {
        if (button == null) return;
        button = GetComponent<Button>();
    }

    protected virtual void AddOnclickEvent()
    {
        this.button.onClick.AddListener(this.OnClick);
    }

    protected abstract void OnClick();
}
