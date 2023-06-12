using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class BaseBtnTab : EventTrigger
{
    // IPointerEnterHandler - OnPointerEnter - Called when a pointer enters the object
    public override void OnPointerEnter(PointerEventData data)
    {
        Debug.Log("OnPointerEnter called.");
    }

    // IPointerExitHandler - OnPointerExit - Called when a pointer exits the object
    public override void OnPointerExit(PointerEventData data)
    {
        Debug.Log("OnPointerExit called.");
    }
    // IPointerDownHandler - OnPointerDown - Called when a pointer is pressed on the object
    public override void OnPointerDown(PointerEventData data)
    {
        Debug.Log("OnPointerDown called.");
    }
    //IPointerUpHandler- OnPointerUp - Called when a pointer is released (called on the GameObject that the pointer is clicking)
    public override void OnPointerUp(PointerEventData data)
    {
        Debug.Log("OnPointerUp called.");
    }
    // IPointerClickHandler - OnPointerClick - Called when a pointer is pressed and released on the same object
    public override void OnPointerClick(PointerEventData data)
    {
        Debug.Log("OnPointerClick called.");
    }

    // IInitializePotentialDragHandler - OnInitializePotentialDrag - Called when a drag target is found, can be used to initialize values

    public override void OnInitializePotentialDrag(PointerEventData data)
    {
        Debug.Log("OnInitializePotentialDrag called.");
    }

    //IBeginDragHandler - OnBeginDrag - Called on the drag object when dragging is about to begin
    public override void OnBeginDrag(PointerEventData data)
    {
        Debug.Log("OnBeginDrag called.");
        //G?i 1 l?n
        //Khi nh?n chu?t,gi? và di chuy?n l?n ??u s? g?i ra hàm này

    }
    // IDragHandler - OnDrag - Called on the drag object when a drag is happening
    public override void OnDrag(PointerEventData data)
    {
        Debug.Log("OnDrag called.");
    }

    //IEndDragHandler - OnEndDrag - Called on the drag object when a drag finishes
    public override void OnEndDrag(PointerEventData data)
    {
        Debug.Log("OnEndDrag called.");
    }


    //IDropHandler - OnDrop - Called on the object where a drag finishes
    public override void OnDrop(PointerEventData data)
    {
        Debug.Log("OnDrop called.");
    }

    // IScrollHandler - OnScroll - Called when a mouse wheel scrolls
    public override void OnScroll(PointerEventData data)
    {
        Debug.Log("OnScroll called.");
    }
    //  IUpdateSelectedHandler - OnUpdateSelected - Called on the selected object each tick
    public override void OnUpdateSelected(BaseEventData eventData)
    {
        // base.OnUpdateSelected(eventData);
    }



    //ISelectHandler - OnSelect - Called when the object becomes the selected object

    public override void OnSelect(BaseEventData data)
    {
        Debug.Log("OnSelect called.");
    }

    //  IDeselectHandler - OnDeselect - Called on the selected object becomes deselected
    public override void OnDeselect(BaseEventData data)
    {
        Debug.Log("OnDeselect called.");
    }
    //IMoveHandler - OnMove - Called when a move event occurs (left, right, up, down)
    public override void OnMove(AxisEventData data)
    {
        Debug.Log("OnMove called.");
    }
    //ISubmitHandler - OnSubmit - Called when the submit button is pressed
    public override void OnSubmit(BaseEventData data)
    {
        Debug.Log("OnSubmit called.");
    }

    // ICancelHandler - OnCancel - Called when the cancel button is pressed
    public override void OnCancel(BaseEventData data)
    {
        Debug.Log("OnCancel called.");

    }





}

