using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Page : MonoBehaviour
{



    public EntryModes mode;
    public Direction direction;
    private void Awake()
    {

    }

    public void Enter(bool input)
    {
        switch (mode)
        {
            case EntryModes.DO_NOTHING:
                break;
            case EntryModes.SLIDE:
                break;
            case EntryModes.ZOOM:
                break;
            case EntryModes.FADE:
                break;

        }
    }
    public void Exit(bool input)
    {
        switch (mode)
        {
            case EntryModes.DO_NOTHING:
                break;
            case EntryModes.SLIDE:
                break;
            case EntryModes.ZOOM:
                break;
            case EntryModes.FADE:
                break;

        }
    }
}
