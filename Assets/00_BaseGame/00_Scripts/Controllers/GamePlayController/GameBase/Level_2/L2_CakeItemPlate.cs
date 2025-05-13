using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class L2_CakeItemPlate : LoadAutoComponents
{
    public CakeType cakeType;
    public SpriteRenderer spriteRenderer;
    public Level_2Ctrl levelCtrl;

    public void Init()
    {
        spriteRenderer.color = new Color32(0,0,0,0);
    }

    void HandleColor()
    {
        spriteRenderer.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var cake = collision.GetComponent<L2_CakeItem>();
        Debug.LogError("test");
        if (cake.cakeType != cakeType) return;
        cake.gameObject.SetActive(false);
        HandleColor();
        levelCtrl.IncreaseAmount();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
