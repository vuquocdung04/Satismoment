using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_67Ctrl : BaseDragController<L67_Tab>
{
    public int currentIndex = 10; // = lsTab[Max].id
    public List<L67_Tab> lsTabs;
    protected override void OnDragEnded()
    {
        
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        
    }

    protected override void OnDragStarted()
    {
        Debug.LogError(draggableComponent.name);
        if (draggableComponent.idTab == currentIndex)
        {
            draggableComponent.DOAnimClosing();
            currentIndex--;
            if(currentIndex == lsTabs[0].idTab - 1)
            {
                StartCoroutine(HandleWinCodition());
            }
        }
    }

    IEnumerator HandleWinCodition()
    {
        isWin = true;
        yield return new WaitForSeconds(0.3f);
        WinBox.SetUp().Show();
    }



    [Button("Setup index Tab",ButtonSizes.Large)]
    void Setup()
    {
        for(int i = 0; i< lsTabs.Count; i++)
        {
            lsTabs[i].idTab = i + 2;
            lsTabs[i].tabRenderer.sortingOrder = lsTabs[i].idTab;
        }
    }
}
