using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L124_Bucket : L124_ObjDragable
{
    public Sprite defaultSprite;
    public Sprite spriteHaveWater;
    public Transform positionSpawn;
    bool isHaveWated;


    public override void HandleCollisionWithObj()
    {
        if (!objCollider.IsTouching(levelCtrl.dir.dirCollider))
        {
            Debug.LogError("Bucket is not collisiton with dir");
            return;
        }
        if (!isHaveWated)
        {
            Debug.LogError("Bucket is not have water");
            return;
        }

        if (!levelCtrl.dir.HasSeeded())
        {
            Debug.LogError("Dir is not have seed");
            return;
        }
        Debug.LogError("Wtf");
        StartCollisionState();
        var positionDir = levelCtrl.dir.transform.position + Vector3.up;
        StartCoroutine(levelCtrl.SpawnTimmingBar(positionDir, delegate
        {
            transform.position = Vector3.zero;
            objRenderer.sprite = defaultSprite;
            isHaveWated = false;
            levelCtrl.dir.HandleGrowSeed();
            EndCollisionState();
        }));
    }


    public void HandleCollisionWithWaterWell()
    {
        if (objCollider.IsTouching(levelCtrl.waterWellCollider))
        {
            StartCollisionState();
            var positionWaterWell = levelCtrl.waterWellCollider.transform.position + Vector3.up;
            StartCoroutine(levelCtrl.SpawnTimmingBar(positionWaterWell, delegate
            {
                transform.position = positionSpawn.position;
                objRenderer.sprite = spriteHaveWater;
                isHaveWated = true;
                EndCollisionState();
            }));
        }
    }

    
    void StartCollisionState()
    {
        objCollider.enabled = false;
        objRenderer.sortingOrder = -1;
    }

    void EndCollisionState()
    {
        objCollider.enabled = true;
        objRenderer.sortingOrder = 5;
    }


}