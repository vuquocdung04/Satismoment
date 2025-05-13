using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3_picture : MonoBehaviour
{
    public Transform icon;

    public SpriteRenderer spritePicture;
    public Sprite sprite1;

    public Coroutine coroutine;
    public CircleCollider2D circleCollider;

    public void AfterWin()
    {
        coroutine = StartCoroutine(ChangeSprite());
    }
    IEnumerator ChangeSprite()
    {
        yield return new WaitForSeconds(0.5f);
        icon.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.6f);
        spritePicture.sprite = sprite1;
        yield return new WaitForSeconds(0.6f);
        WinBox.SetUp().Show();
    }

    private void OnDestroy()
    {
        StopCoroutine(coroutine);
    }
}
