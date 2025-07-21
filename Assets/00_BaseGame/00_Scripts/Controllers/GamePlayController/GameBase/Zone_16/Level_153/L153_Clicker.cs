using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L153_Clicker : MonoBehaviour
{
    public SpriteRenderer objRenderer;
    public List<Sprite> lsFrames;
    public bool coolDown;

    public void OnStateStart()
    {
        StartCoroutine(Clicking());
    }

    IEnumerator Clicking()
    {
        coolDown = true;
        objRenderer.sprite = lsFrames[1];
        yield return new WaitForSeconds(0.1f);
        objRenderer.sprite = lsFrames[0];
        coolDown = false;
    } 
}
