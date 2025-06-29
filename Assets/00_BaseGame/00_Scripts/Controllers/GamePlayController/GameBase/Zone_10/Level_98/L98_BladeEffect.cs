using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L98_BladeEffect : MonoBehaviour
{
    public TrailRenderer trailRenderer;
    public CircleCollider2D circleCollider;
    public Level_98Ctrl levelCtrl;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        levelCtrl.winProgress++;
        var effectClone = SimplePool2.Spawn(levelCtrl.hitEffect,collision.transform.position, Quaternion.identity);
        StartCoroutine(effectClone.DesSpawn());
    }

    public void OnStart()
    {
        trailRenderer.enabled = true;
        circleCollider.enabled = true;
    }
    public void OnEnd()
    {
        trailRenderer.enabled = false;
        circleCollider.enabled = false;
    }
}
