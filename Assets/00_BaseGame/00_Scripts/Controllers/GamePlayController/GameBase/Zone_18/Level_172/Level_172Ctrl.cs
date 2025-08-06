using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_172Ctrl : MonoBehaviour
{
    public Transform knife;
    public Transform waterMelonLeft;
    public Transform waterMelonRight;
    public L172_Effect effectPrefabs;
    public List<Transform> lsPoints;
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            knife.DOMoveY(-0.8f,0.2f);
            waterMelonLeft.DOMoveX(-1.35f,0.3f);
            waterMelonRight.DOMoveX(1.35f, 0.3f);
            StartCoroutine(SpawnEffect());
        }
    }

    IEnumerator SpawnEffect()
    {
        yield return new WaitForSeconds(0.3f);
        for(int i = 0; i < 2; i++)
        {
            for(int j = 0; j < lsPoints.Count; j++)
            {
                var effectClone = SimplePool2.Spawn(effectPrefabs, lsPoints[j].position, Quaternion.identity);
                effectClone.InitState();
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(0.2f);
        WinBox.SetUp().Show();
    }
}
