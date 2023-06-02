using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PageDot : MonoBehaviour
{
    [Header("Event")]
    public UnityEvent<bool> OnActiveStateChange;
    public UnityEvent<int> onPress;

    public bool IsActive { get;private set; }
    public int Index { get;  set; }

    private void Start()
    {
        ChangeActiveState(Index == 0);
    }
    public virtual void ChangeActiveState(bool isActive)
    {
        IsActive = isActive;
        OnActiveStateChange?.Invoke(isActive);
    }

    public void Press()
    {
        onPress?.Invoke(Index);
    }
}
