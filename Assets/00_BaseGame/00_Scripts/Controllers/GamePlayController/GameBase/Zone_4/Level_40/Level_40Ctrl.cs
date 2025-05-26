using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_40Ctrl : MonoBehaviour
{
    public bool isWin = false;
    public Transform prefab;
    public Transform mask;
    public float speedMask = 0.2f;
    public SpriteRenderer rabbit;
    public List<Transform> lsFruice;
    public List<L40_Item> lsItems;
    public List<Sprite> lsRabbit;
    public int i = 0;
    public int j = 0;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(j < 2)
            {
                lsItems[i].DoFalling();
                i++;
            }
            else if(j < 8)
            {
                SpawnRandPrefab();
            }
            else if(j < 11) // 3 dau tay
            {
                lsItems[i].DoFalling();
                i++;
            }
            else if(j < 17)
            {
                SpawnRandPrefab();
            }
            else if(j < 23)
            {
                lsItems[i].DoFalling();
                i++;
            }
            else
            {
                SpawnRandPrefab();
            }

            if (j < 29)
            {
                j++;
            }
            else
            {
                isWin = true;
                StartCoroutine(HandleWinCodition());
            }
        }
    }

    IEnumerator HandleWinCodition()
    {
        var waitTime = new WaitForSeconds(0.3f);
        int i = 0;
        while(i < lsFruice.Count)
        {
            lsFruice[i].gameObject.SetActive(true);
            lsFruice[i].DOMoveY(0.59f, 0.3f);
            yield return waitTime;
            i++;
        }
        yield return new WaitForSeconds(0.8f);
        rabbit.gameObject.SetActive(true);
        rabbit.transform.DOMoveY(1.09f,0.3f);
        yield return new WaitForSeconds(0.31f);
        rabbit.sprite = lsRabbit[0];
        yield return new WaitForSeconds(0.3f);
        rabbit.sprite = lsRabbit[1];
        yield return new WaitForSeconds(0.3f);
        rabbit.sprite = lsRabbit[2];
        yield return new WaitForSeconds(1f);
        WinBox.SetUp().Show();
    }

    void SpawnRandPrefab()
    {
        float randPos = Random.Range(-0.5f, 0.6f);
        var fakeWater = SimplePool2.Spawn(prefab, new Vector3(randPos,randPos + 2.4f,0), Quaternion.identity);
        fakeWater.DOMoveY(-1.58f, 0.5f).OnComplete(delegate
        {
            SimplePool2.Despawn(fakeWater.gameObject);
            MoveMaskUp();
        });
    }
    void MoveMaskUp()
    {
        mask.position += new Vector3(0, speedMask, 0);
    }
    [Button("Setup", ButtonSizes.Large)]
    void EditorSetup()
    {
        foreach(var item in this.lsItems)
        {
            item.posCorrect = item.transform.position;
        }
    }
}
