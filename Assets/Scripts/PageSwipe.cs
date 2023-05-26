using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PageSwipe : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 _panelLocation;
    private void Start()
    {
        _panelLocation = transform.position;
    }
    public void OnDrag(PointerEventData data)
    {
        float difference = data.pressPosition.x - transform.position.x;
        transform.position = _panelLocation - new Vector3(difference, 0, 0);
    }
    public void OnEndDrag(PointerEventData data)
    {
        _panelLocation = transform.position;
    }
}
