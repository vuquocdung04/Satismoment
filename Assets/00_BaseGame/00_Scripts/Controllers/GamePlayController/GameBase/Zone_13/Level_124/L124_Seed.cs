using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class L124_Seed : MonoBehaviour
{
    public Level_124Ctrl levelCtrl;
    public BoxCollider2D boxCollider2d;
    public SpriteRenderer spriteRenderer;
    public List<Sprite> lsFrames;
    public List<Transform> lsPointSpawnApples;
    public bool inDir;
    bool CheckCollisionWithDir()
    {
        if(boxCollider2d.IsTouching(levelCtrl.dirCollider)) return true;
        return false;
    }

    public void HandleCollisitonWithDir()
    {
        if (CheckCollisionWithDir())
        {
            inDir = true;
            transform.position = levelCtrl.dirCollider.transform.position;
            spriteRenderer.sprite = lsFrames[1];
        }
    }
    public void ResetSeedState()
    {
        spriteRenderer.sprite = lsFrames[0];
        indexFrame = 1;
        spriteRenderer.sprite = lsFrames[0];
        inDir = false;
    }

    int indexFrame = 1;
    public void GrownSeed()
    {
        indexFrame++;
        if(indexFrame == lsFrames.Count)
        {
            ResetSeedState();
            levelCtrl.SpawnApple(lsPointSpawnApples);
            SimplePool2.Despawn(gameObject);
            return;
        }
        spriteRenderer.sprite = lsFrames[indexFrame];
    }
}
