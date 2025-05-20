using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine.XR;
public class Level_23Ctrl : BaseDragController<L23_Picture>
{
    public Transform mask;
    public float startPosY;
    public float pictureHeight;
    public List<L23_Picture> lsPictures;
    public bool isWin = false;
    Vector3 newPos;

    // pos max mouse
    public float minYClamp = -3f;
    public float maxYClamp = 3f;

    float targetY;
    private void Start()
    {
        HandleSortPicture(null, true);
    }

    protected override void Update()
    {
        if (isWin) return;
        base.Update();
    }


    protected override void OnDragStarted()
    {
        base.OnDragStarted();
    }

    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {

        newPos = draggableComponent.transform.position;
        newPos.y += deltaMousePosition.y;
        newPos.x = 0;

        targetY = mouseWorldPos.y;
        targetY = Mathf.Clamp(targetY, minYClamp, maxYClamp);
        newPos.y = Mathf.Clamp(mouseWorldPos.y, minYClamp, maxYClamp);


        draggableComponent.transform.position = newPos;
        lsPictures.Sort((a, b) => a.transform.position.y.CompareTo(b.transform.position.y));

        HandleSortPicture(draggableComponent, false);

    }

    protected override void OnDragEnded()
    {
        base.OnDragEnded();
        HandleSortPicture(null, true);
        lsPictures.Sort((a, b) => b.transform.position.y.CompareTo(a.transform.position.y));


        HandleWin();

    }


    void HandleSortPicture(L23_Picture draggedPicture, bool snapPosition = false)
    {
        float currentEdgeY = this.startPosY;
        L23_Picture picture = null;
        Vector3 targetPosition;
        for (int i = 0; i < lsPictures.Count; i++)
        {
            picture = lsPictures[i];
            if (picture == null) continue;
            float targetBookCenterY = currentEdgeY + pictureHeight / 2;

            targetPosition = new Vector3(0, targetBookCenterY, 0);

            if (picture == draggedPicture && !snapPosition)
            {
                //TODO
            }
            else
            {
                if (snapPosition) picture.transform.position = targetPosition;
                else picture.transform.position = Vector3.Lerp(picture.transform.position, targetPosition, 10f * Time.deltaTime);
            }
            currentEdgeY += pictureHeight;

        }
    }

    void HandleWin()
    {
        if (!CheckWinCondition()) return;
        isWin = true;
        mask.DOLocalMoveY(0.27f,3f);
        DOVirtual.DelayedCall(3f, () => WinBox.SetUp().Show());

    }


    bool CheckWinCondition()
    {
        for (int i = 0; i < lsPictures.Count; i++)
        {
            if (lsPictures[i].idPicture != i) return false;
        }
        return true;
    }

    [Button("Setup",ButtonSizes.Large)]
    void SetUp()
    {
        pictureHeight = lsPictures[0].spriteRenderer.bounds.size.y;
        this.startPosY = lsPictures[0].posDefault.y - pictureHeight / 2f;

    }
}
