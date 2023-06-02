using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BannerView : MonoBehaviour
{
    public UnityEvent OnChangingToActiveState;
    public UnityEvent OnChangingToInactiveState;
    public UnityEvent<bool> OnActiveStateChange;

    public void ChangingToActiveState()
    {
        OnChangingToActiveState?.Invoke();
    }

    public void ChangingInactiveState()
    {
        OnChangingToInactiveState?.Invoke();
    }

    public void ChangingActiveState(bool active)
    {
        OnActiveStateChange?.Invoke(active);
    }
}
