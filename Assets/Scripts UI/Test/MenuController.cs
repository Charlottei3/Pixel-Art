using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Page InitialPage;
    private GameObject FirstForcusObject;
    private Canvas rootCanvas;
    private Stack<Page> pageStack = new Stack<Page>();
    private void Awake()
    {
        rootCanvas = GetComponent<Canvas>();
    }
    private void Start()
    {
        if (FirstForcusObject != null)
        {
            EventSystem.current.SetSelectedGameObject(FirstForcusObject);
        }
        if (InitialPage != null)
        {
            PushPage(InitialPage);
        }
    }
    private void OnCancel()
    {
        if (rootCanvas.enabled && rootCanvas.gameObject.activeInHierarchy)
        {
            if (pageStack.Count != 0)
            {
                PopPage();
            }
        }
    }
    public bool isPageInStack(Page page)

    {
        return pageStack.Contains(page);
    }
    public bool isPageOnTopStack(Page page)
    {
        return pageStack.Count > 0 && page == pageStack.Peek();
    }
    public void PushPage(Page page)
    {
        page.Enter(true);
        if (pageStack.Count > 0)
        {
            Page currentPage = pageStack.Peek();
            if (false)
            {
                currentPage.Exit(false);
            }
        }
        pageStack.Push(page);
    }
    public void PopPage()
    {
        if (pageStack.Count > 1)
        {
            Page page = pageStack.Pop();
            page.Exit(true);
            Page newCurrentPage = pageStack.Peek();
            if (false)
            {
                newCurrentPage.Enter(false);
            }

        }
        else
        {
            Debug.Log("Trying to pop but trust have only 1 page");

        }
    }
}
