using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L7_IceCream : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public BoxCollider2D boxCollider;
    public Sprite spriteWin;
    public List<Sprite> lsSprites;
    public Coroutine corotine;
    private void Start()
    {
        corotine = StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        var waitTime = new WaitForSeconds(0.3f);
        int i = 0;
        while (true)
        {
            spriteRenderer.sprite = lsSprites[i];
            yield return waitTime;
            i++;

            if(i >= lsSprites.Count) i = 0;
        }
    }


}
