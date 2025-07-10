using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L124_Apple : MonoBehaviour
{
    public Level_124Ctrl levelCtrl;
    public BoxCollider2D objCollider;
    public SpriteRenderer spriteRenderer;
    public Sprite bigSprite;
    bool CheckCollisionWithPenguin()
    {
        if(objCollider.IsTouching(levelCtrl.penguin.objCollider)) return true;
        return false;
    }

    public void HandleCollisionWithPenguin()
    {
        if (CheckCollisionWithPenguin())
        {
            StartCollisionState();
            var penguinPosition = levelCtrl.penguin.transform.position + new Vector3(0,1,0);
            var seedPostion = levelCtrl.penguin.transform.position + new Vector3(1, 0, 0);
            StartCoroutine(levelCtrl.SpawnTimmingBar(penguinPosition, delegate
            {
                levelCtrl.SpawnSeed(seedPostion);
                objCollider.enabled = true;
                gameObject.SetActive(false);
            }));
        }
    }

    void StartCollisionState()
    {
        objCollider.enabled = false;
        spriteRenderer.sortingOrder = -1;
    }

    public void InitState()
    {
        objCollider.enabled = true;
        spriteRenderer.sortingOrder = 5;
    }


    public bool CheckTochingWithZone()
    {
        if (objCollider.IsTouching(levelCtrl.canvas.boxCollider2d))
        {
            // Gọi hàm và nhận trực tiếp vị trí đích
            Vector3 targetPos = levelCtrl.canvas.AssignItemToNearestAvailable(this, transform.position);

            // Kiểm tra xem có vị trí hợp lệ được trả về hay không
            if (targetPos != Vector3.zero) // Kiểm tra nếu vị trí không phải là Vector3.zero (giá trị báo lỗi)
            {
                transform.DOMove(targetPos, 0.2f).SetEase(Ease.OutQuad);
                spriteRenderer.sprite = bigSprite;
                return true;
            }
        }
        return false;
    }

}
