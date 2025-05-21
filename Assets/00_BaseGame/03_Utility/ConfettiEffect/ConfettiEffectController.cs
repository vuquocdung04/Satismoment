using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiEffectController : MonoBehaviour
{
    public RectTransform parentPool;
    public List<ConfettiEffect> lsPoolEffect_UIs;
    public List<Sprite> lsEffects;
    public ConfettiEffect confettiPrefab;

    public void SpawmEffect_Drop_UI(Transform posSpawn, bool isUI = false)
    {
        StartCoroutine(SpawnConfettiRoutine(posSpawn, isUI));
    }

    IEnumerator SpawnConfettiRoutine(Transform posSpawn, bool isUI = false)
    {
        int rand = Random.Range(20,25);
        Vector3 m_posSpawn = Vector3.zero;
        var waitTime = new WaitForSeconds(0.04f);
        for (int i = 0; i < rand; i++)
        {
                m_posSpawn = new Vector3(posSpawn.position.x + Random.Range(-4, 4),
                    posSpawn.position.y, posSpawn.position.z);
            ConfettiEffect confettiEffect = GetConfettiFromPool();
            confettiEffect.transform.SetParent(parentPool);
            confettiEffect.gameObject.SetActive(true);

            if (!isUI)
                confettiEffect.transform.position = Camera.main.WorldToScreenPoint(m_posSpawn);
            else
            {
                m_posSpawn = new Vector3(posSpawn.position.x + Random.Range(-350, 350),
                    posSpawn.position.y, posSpawn.position.z);
                confettiEffect.transform.position = m_posSpawn;
            }

            confettiEffect.SetSpriteIcon(lsEffects);
            confettiEffect.SetRandomFallEffect();
            yield return waitTime;

        }
    }



    private ConfettiEffect GetConfettiFromPool()
    {
        if(lsPoolEffect_UIs == null)
            lsPoolEffect_UIs = new List<ConfettiEffect>();

        try
        {
            for(int i = 0; i < lsPoolEffect_UIs.Count; i++)
            {
                if (!lsPoolEffect_UIs[i].gameObject.activeSelf) return lsPoolEffect_UIs[i];
            }
        }
        catch
        {
            var effect1 = Instantiate(lsPoolEffect_UIs[0], parentPool);
            lsPoolEffect_UIs.Add(effect1);
            return effect1;
        }

        var effect = Instantiate(confettiPrefab, parentPool);
        lsPoolEffect_UIs.Add(effect);
        return effect;

    }

}
