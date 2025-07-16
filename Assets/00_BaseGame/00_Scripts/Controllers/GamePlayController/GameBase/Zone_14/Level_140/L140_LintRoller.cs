using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L140_LintRoller : MonoBehaviour
{
    public Level_140Ctrl levelCtrl;
    public Transform effect;
    public float size;
    public void MoveEffect(float moveAmount)
    {
        effect.localPosition -= new Vector3(0,moveAmount,0); 
        if(effect.localPosition.y >= size)
        {
            effect.localPosition = Vector3.zero;
        }
        else if(effect.localPosition.y <= -size)
        {
            effect.localPosition = Vector3.zero;
        }
    }

    [Button("Setup",ButtonSizes .Large)]
    void Setup()
    {
        size = effect.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var clothScrap = collision.GetComponent<L140_ClothScraps>();
        if (clothScrap == null) return;
        if (clothScrap.isOverlapping) return;

        levelCtrl.currentClothScrapsCount++;
        clothScrap.transform.SetParent(effect);
        float randX = Random.Range(-0.3f,0.3f);
        clothScrap.transform.localPosition = new Vector3(randX, 0);
        clothScrap.objRenderer.sortingOrder = 8;
        clothScrap.objRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
    }
}
