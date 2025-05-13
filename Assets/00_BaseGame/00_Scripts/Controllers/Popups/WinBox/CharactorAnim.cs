using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactorAnim : MonoBehaviour
{
    public Image imgCharactor;
    public List<Sprite> lsSprite1s;
    public List<Sprite> lsSprite2s;
    public List<Sprite> lsSprite3s;
    public List<Sprite> lsSprite4s;
    public List<Sprite> lsSprite5s;

    private Coroutine corotine;
    List<Sprite> selectedSpriteList;

    public void Init()
    {
        corotine = StartCoroutine(AnimationWin());
    }

    IEnumerator AnimationWin()
    {
        var time = new WaitForSeconds(0.15f);
        int i = 0;
        int rand = Random.Range(0,5);

        switch (rand)
        {
            case 0:
                selectedSpriteList = lsSprite1s;
                break;
            case 1:
                selectedSpriteList = lsSprite2s;
                break;
            case 2:
                selectedSpriteList = lsSprite3s;
                break;
            case 3:
                selectedSpriteList = lsSprite4s;
                break;
            case 4:
                selectedSpriteList = lsSprite5s;
                break;
        }

        while (true)
        {
            imgCharactor.sprite = selectedSpriteList[i];
            imgCharactor.SetNativeSize();
            yield return time;
            i++;
            if (i >= selectedSpriteList.Count) i = 0;
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(corotine);
    }

}
