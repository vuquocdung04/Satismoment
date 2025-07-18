using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L146_News : MonoBehaviour
{
    public Level_146Ctrl levelCtrl;
    public L146_Effect effect;
    public Transform posSpawn;

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    public IEnumerator Spawn()
    {
        var waitTime = new WaitForSeconds(0.75f);
        while (!levelCtrl.isWin)
        {
            for(int i = 0; i < 2; i++)
            {
                var effectClone = SimplePool2.Spawn(effect);
                effectClone.transform.SetParent(transform);
                effectClone.Init(posSpawn);
                yield return waitTime;
            }
            
        }
    }
}
