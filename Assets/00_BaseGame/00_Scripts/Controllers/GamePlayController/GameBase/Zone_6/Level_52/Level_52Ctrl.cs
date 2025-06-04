using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_52Ctrl : BaseDragController<L52_Picture>
{
    public int winProgress;
    public List<L52_Picture> lsPicture;
    public Transform prefabCircleRed;
    public Transform mask;
    public List<float> lsTargetMoveX;
    protected override void OnDragEnded()
    {
        StartCoroutine(HandleWinCodition());
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        
    }

    protected override void OnDragStarted()
    {
        int closestPointIndex = 0;
        Vector2 pos = draggableComponent.lsCollider[0].offset + (Vector2)draggableComponent.transform.position;
        float minDistance = Vector2.Distance(pos, mouseWorldPos);
        for(int i = 1; i < draggableComponent.lsCollider.Count; i++)
        {
            Vector2 currentPointPos = draggableComponent.lsCollider[i].offset + (Vector2)draggableComponent.transform.position;
            float currentDistance = Vector2.Distance(currentPointPos, mouseWorldPos);
            if (currentDistance < minDistance)
            {
                closestPointIndex = i;
                minDistance = currentDistance;
            }
        }
        SpawnRedCircle(closestPointIndex);
    }

    void SpawnRedCircle(int index)
    {
        for(int i = 0; i < lsPicture.Count; i++)
        {
            Vector2 posSpawn = lsPicture[i].lsCollider[index].offset + (Vector2)lsPicture[i].transform.position;
            Instantiate(prefabCircleRed, posSpawn, Quaternion.identity);
            lsPicture[i].lsCollider[index].enabled = false;
        }
        winProgress++;
        HandleMoveMask();
    }

    void HandleMoveMask()
    {
        mask.DOMoveX(lsTargetMoveX[winProgress - 1], 1f);
    }
    [Button("Setup target move X", ButtonSizes.Large)]
    void SetupTargetMove(float amount)
    {
        lsTargetMoveX.Clear();
        for(int i = 0; i < 5; i++)
        {
            lsTargetMoveX.Add(amount  + amount * i);
        }
    }

    IEnumerator HandleWinCodition()
    {
        if(winProgress  == 5)
        {
            isWin = true;
            yield return new WaitForSeconds(0.4f);
            WinBox.SetUp().Show();
        } 
    }
}
