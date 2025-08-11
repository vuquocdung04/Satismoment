using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using _00_BaseGame._00_Scripts.Controllers.GamePlayController.GameBase;
using UnityEngine;

public class Level_146Ctrl : BaseDragController<L146_SocialMedia>
{
    public List<L146_IconHeart> lsIconHearts;
    public Sprite heartActive;
    public int likedPostCount;
    protected override void OnDragEnded()
    {

    }

    Vector3 newPos;
    protected override void OnDragLogic(Vector3 currentMousePosition, Vector3 deltaMousePosition)
    {
        newPos = draggableComponent.socialMedia.transform.position + new Vector3(0,mouseDelta.y,0);
        newPos.y = Mathf.Clamp(newPos.y,0,16);
        draggableComponent.socialMedia.transform.position = newPos;
    }

    protected override void OnDragStarted()
    {
        
    }

    public IEnumerator HandleWinCondition()
    {
        if(likedPostCount == lsIconHearts.Count)
        {
            isWin = true;
            yield return new WaitForSeconds(0.5f);
            WinBox.SetUp().Show();
        }
    }


    [Button("Setup icon Heart", ButtonSizes.Large)]
    void SetupIcon()
    {
        foreach(var icon in this.lsIconHearts)
        {
            icon.objCollider = icon.transform.GetComponent<BoxCollider2D>();
            icon.levelCtrl = this;
            icon.objRenderer = icon.transform.GetComponent<SpriteRenderer>();
        }
    }
}
