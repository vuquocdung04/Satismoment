using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L124_Seed : L124_ObjDragable
{
    public List<L124_Apple> lsApples;
    public List<Sprite> lsFrames;
    public List<Transform> lsPointSpawnApples;

    public override void HandleCollisionWithObj()
    {
        if (!objCollider.IsTouching(levelCtrl.dir.dirCollider)) return;
        if (levelCtrl.dir.HasSeeded()) return;
        transform.position = levelCtrl.dir.transform.position;
        objCollider.enabled = false;
        levelCtrl.dir.SetCurrentSeed(this);
        objRenderer.sprite = lsFrames[1];
    }
    public void ResetSeedState()
    {
        lsApples.Clear();
        objRenderer.sprite = lsFrames[0];
        indexFrame = 1;
        objCollider.enabled = true;
    }

    int indexFrame = 1;
    public void GrownSeed()
    {
        indexFrame++;
        if(indexFrame == lsFrames.Count - 1)
        {
            levelCtrl.SpawnApple(lsPointSpawnApples);
        }
        if (indexFrame == lsFrames.Count)
        {
            levelCtrl.dir.OnSeedGrowthComplete();
            JumpingApple();
            ResetSeedState();
            SimplePool2.Despawn(gameObject);
            return;
        }
        objRenderer.sprite = lsFrames[indexFrame];
    }

    public void JumpingApple()
    {
        foreach(var apple in this.lsApples)
        {
            Debug.LogError("Jump");
            apple.objCollider.enabled = true;
            Transform appleTransform = apple.transform;
            appleTransform
                .DOJump(appleTransform.position, // giữ nguyên vị trí x/z
                        1f, // chiều cao jump
                        1, // số lần jump (1 lần)
                        0.5f) // thời gian
                .SetEase(Ease.OutBounce); // Hiệu ứng bật
        }
    }

}
